using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : Obstacle
{
    //[SerializeField] private float m_Height;
    //[SerializeField] private float m_MoveSpeed;

    //public float Height
    //{
    //    get
    //    {
    //        return m_Height;
    //    }
    //}

    private void Update()
    {
        Move();
    }
}
