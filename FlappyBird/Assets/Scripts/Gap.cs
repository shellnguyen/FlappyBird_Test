using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : MonoBehaviour
{
    [SerializeField] private float m_Height;
    [SerializeField] private float m_MoveSpeed;

    public float Height
    {
        get
        {
            return m_Height;
        }
    }

    public void SetHeight(float height)
    {
        m_Height = height;
    }

    private void Update()
    {
        transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
    }
}
