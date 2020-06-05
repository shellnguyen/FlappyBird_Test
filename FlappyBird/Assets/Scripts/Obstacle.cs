using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float m_Height;
    [SerializeField] protected float m_MoveSpeed;

    public float Height
    {
        get
        {
            return m_Height;
        }
    }

    protected virtual void Move()
    {
        transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
    }

    public virtual void SetSize(float height)
    {
        m_Height = height;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
