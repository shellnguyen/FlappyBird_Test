using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_Renderer;
    [SerializeField] private Vector3 m_Position;
    [SerializeField] private bool m_IsAlive;

    [SerializeField] private float m_Acceleration;
    [SerializeField] private float m_Velocity;
    [SerializeField] private float m_PositionY;
    [SerializeField] private float m_Gravity;

    // Start is called before the first frame update
    private void Start()
    {
        m_Position = transform.position;
        m_PositionY = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_Acceleration = 0.0f;
            m_Velocity = m_Gravity / 4.0f;
        }
        else
        {
            m_Acceleration -= m_Gravity * Time.deltaTime;
        }

        if(m_Acceleration >= m_Gravity)
        {
            m_Acceleration = m_Gravity;
        }

        m_Velocity += m_Acceleration * Time.deltaTime;
        m_PositionY += m_Velocity * Time.deltaTime;

        m_Position.y = m_PositionY;
        transform.position = m_Position;
    }
}
