using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] BirdController m_Bird;
    [SerializeField] float m_ScreenWidth;
    [SerializeField] float m_ScreenHeight;

    [SerializeField] List<BlockController> m_Blocks;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SpawnBlock()
    {
    }
}
