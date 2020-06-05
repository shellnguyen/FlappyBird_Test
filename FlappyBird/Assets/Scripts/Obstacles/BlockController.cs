using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : Obstacle
{
    [SerializeField] private GameObject SingleBlock;
    [SerializeField] private float m_SingleBlockHeight;

    private void BuildBlock()
    {
        int blocksToSpawn = Mathf.FloorToInt(m_Height / m_SingleBlockHeight);
        Vector3 position = transform.position;
        for(int i = 0; i < blocksToSpawn; ++i)
        {
            Instantiate(SingleBlock, transform).transform.position = position;
            position.y += 1.0f;
        }
    }

    public override void SetSize(float height)
    {
        base.SetSize(height);
        BuildBlock();
    }
}
