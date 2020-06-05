using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private Vector2 m_ScreenBounds;

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
        //m_ScreenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height - 90, MainCamera.transform.position.z));
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Width = m_Renderer.bounds.extents.x;
        m_Height = m_Renderer.bounds.extents.y;

        m_Position = transform.position;
        m_PositionY = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        transform.position = m_Position;
    }

    private void LateUpdate()
    {
        m_Position.x = Mathf.Clamp(m_Position.x, m_ScreenBounds.x * -1 + m_Width, m_ScreenBounds.x - m_Width);
        m_Position.y = Mathf.Clamp(m_Position.y, m_ScreenBounds.y * -1 + m_Height, m_ScreenBounds.y - m_Height);
        transform.position = m_Position;
    }

    public void SetScreenBounds(Vector2 bounds)
    {
        m_ScreenBounds = bounds;
    }
}
