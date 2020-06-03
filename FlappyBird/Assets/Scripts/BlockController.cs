﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private GameObject SingleBlock;
    [SerializeField] private float m_SingleBlockHeight;
    [SerializeField] private float m_Height;
    [SerializeField] private float m_MoveSpeed;

    // Start is called before the first frame update
    private void OnEnable()
    {
        BuildBlock();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
    }

    private void BuildBlock()
    {
        int blocksToSpawn = Mathf.FloorToInt(m_Height / m_SingleBlockHeight);
        Vector3 position = transform.position;
        for(int i = 0; i < blocksToSpawn; ++i)
        {
            position.y =  (float)i;
            Instantiate(SingleBlock, position, Quaternion.identity, transform);
        }
    }
}
