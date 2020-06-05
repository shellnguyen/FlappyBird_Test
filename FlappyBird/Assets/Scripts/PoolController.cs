using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : Singleton<PoolController>
{
    private Dictionary<int, Queue<GameObject>> m_ObjectPool;

    public void AddObject()
    {
        
    }
}
