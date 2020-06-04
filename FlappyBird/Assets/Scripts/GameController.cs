using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool m_IsStart;

    [SerializeField] private BirdController m_Bird;
    [SerializeField] private float m_ScreenWidth;
    [SerializeField] private float m_ScreenHeight;

    [SerializeField] private List<BlockController> m_Blocks;
    [SerializeField] private GameObject m_BlockPrefab;
    [SerializeField] private GameObject m_SpawnPoint;

    //[SerializeField] private Li

    [SerializeField] private float m_TopEdgeY;
    [SerializeField] private float m_BottomEdgeY;
    [SerializeField] private float m_Time;

    // Start is called before the first frame update
    private void Start()
    {
        m_IsStart = false;
        m_Time = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!m_IsStart)
        {
            return;
        }

        if(m_Blocks.Count < 20 && (Time.time - m_Time) > 8.0f)
        {
            SpawnBlock();
        }
    }

    private void SpawnBlock()
    {
        float topHeight = Random.Range(1, 6) * 2;
        float gapHeight = Random.Range(2, 5) * 2;
        float bottomHeight = 20.0f - topHeight - gapHeight;

        //Spawn Top
        GameObject topBlock = Instantiate(m_BlockPrefab, m_SpawnPoint.transform.position, Quaternion.identity);
        topBlock.transform.Translate(0.0f, m_TopEdgeY - (topHeight / 2) + 0.5f, 0.0f);
        BlockController topBlockScripts = topBlock.GetComponent<BlockController>();
        topBlockScripts.SetSize(topHeight);
        m_Blocks.Add(topBlockScripts);

        //Spawn Down
        GameObject bottomBlock = Instantiate(m_BlockPrefab, m_SpawnPoint.transform.position, Quaternion.identity);
        bottomBlock.transform.Translate(0.0f, m_BottomEdgeY + 0.5f, 0.0f);
        BlockController bottomBlockScript = bottomBlock.GetComponent<BlockController>();
        bottomBlockScript.SetSize(bottomHeight);
        m_Blocks.Add(bottomBlockScript);
        m_Time = Time.time;
    }
}
