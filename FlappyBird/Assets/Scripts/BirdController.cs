﻿using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private Vector3 m_OriginalPosition;
    [SerializeField] private Vector2 m_ScreenBounds;
    [SerializeField] private float m_YFloorSize;

    [SerializeField] private Animator m_Animator;
    [SerializeField] private int m_HitTypeParamId;

    [SerializeField] private SpriteRenderer m_Renderer;
    [SerializeField] private Vector3 m_Position;
    [SerializeField] private bool m_IsAlive;

    [SerializeField] private float m_Acceleration;
    [SerializeField] private float m_Velocity;
    [SerializeField] private float m_PositionY;
    [SerializeField] private float m_Gravity;

    [SerializeField] private float m_Width;
    [SerializeField] private float m_Height;

    // Start is called before the first frame update
    private void Start()
    {
        m_IsAlive = true;

        m_Animator = GetComponent<Animator>();
        m_HitTypeParamId = Animator.StringToHash("HitType");

        m_Renderer = GetComponent<SpriteRenderer>();
        m_Width = m_Renderer.bounds.extents.x;
        m_Height = m_Renderer.bounds.extents.y;

        m_OriginalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        m_Position = transform.position;
        m_PositionY = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && m_IsAlive)
        {
            Utilities.Instance.DispatchEvent(Shell.Event.PlayAudio, "play_one", 1);
            m_Acceleration = 0.0f;
            m_Velocity = m_Gravity / 4.0f;
        }
        else
        {
            m_Acceleration -= m_Gravity * Time.deltaTime;
        }

        if (m_Acceleration >= m_Gravity)
        {
            m_Acceleration = m_Gravity;
        }

        m_Velocity += m_Acceleration * Time.deltaTime;
        m_PositionY += m_Velocity * Time.deltaTime;

        m_Position.y = m_PositionY;
        //transform.position = m_Position;

        m_Position.x = Mathf.Clamp(m_Position.x, m_ScreenBounds.x * -1 + m_Width, m_ScreenBounds.x - m_Width);
        m_Position.y = Mathf.Clamp(m_Position.y, m_ScreenBounds.y * -1 + m_Height + m_YFloorSize, m_ScreenBounds.y - m_Height);
        transform.position = m_Position;
    }

    public void SetScreenBounds(Vector2 bounds, float yFloorSize)
    {
        m_ScreenBounds = bounds;
        m_YFloorSize = yFloorSize;
    }

    public void Hit(int hitType)
    {
        m_IsAlive = false;
        m_Animator.SetInteger(m_HitTypeParamId, hitType);
    }

    public void OnReset()
    {
        transform.position = m_OriginalPosition;
        m_Position = m_OriginalPosition;
        m_PositionY = m_OriginalPosition.y;
        m_Acceleration = 0.0f;
        m_Velocity = 0.0f;

        m_Animator.SetInteger(m_HitTypeParamId, 0);
        m_IsAlive = true;
    }
}
