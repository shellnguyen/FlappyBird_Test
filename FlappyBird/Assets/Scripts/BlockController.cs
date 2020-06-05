using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : Obstacle
{
    [SerializeField] private GameObject SingleBlock;
    [SerializeField] private float m_SingleBlockHeight;
    //[SerializeField] private float m_Height;
    //[SerializeField] private float m_MoveSpeed;

    //public float Height
    //{
    //    get
    //    {
    //        return m_Height;
    //    }
    //}

    // Start is called before the first frame update
    private void OnEnable()
    {
        //BuildBlock();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

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
