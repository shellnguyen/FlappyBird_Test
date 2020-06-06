using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float m_Height;
    [SerializeField] public bool IsHitRecently;

    public float Height
    {
        get
        {
            return m_Height;
        }
    }

    public virtual void SetSize(float height)
    {
        m_Height = height;
    }
}
