using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private const float BLOCK_YOFFSET = 0.5f;
    private const float BIRD_YOFFSET = 0.4f;

    [SerializeField] private bool m_IsStart;

    [SerializeField] private BirdController m_Bird;
    [SerializeField] private float m_ScreenWidth;
    [SerializeField] private float m_ScreenHeight;

    [SerializeField] private List<BlockController> m_Blocks;
    [SerializeField] private GameObject m_BlockPrefab;

    [SerializeField] private List<Gap> m_Gaps;
    [SerializeField] private GameObject m_GapPrefab;
    [SerializeField] private GameObject m_SpawnPoint;

    //[SerializeField] private Li

    [SerializeField] private float m_TopEdgeY;
    [SerializeField] private float m_BottomEdgeY;
    [SerializeField] private float m_Time;

    [SerializeField] private GameObject m_Floor;

    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [SerializeField] private int m_Score;

    // Start is called before the first frame update
    private void Start()
    {
        m_IsStart = true;
        m_Score = 0;
        m_Time = Time.time;
        m_Blocks = new List<BlockController>();
        m_Gaps = new List<Gap>();

        StartCoroutine(CheckCollision());
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
        topBlock.name = "TopBlock";
        topBlock.transform.Translate(0.0f, m_TopEdgeY - (topHeight / 2) + BLOCK_YOFFSET, 0.0f);
        BlockController topBlockScripts = topBlock.GetComponent<BlockController>();
        topBlockScripts.SetSize(topHeight);
        m_Blocks.Add(topBlockScripts);

        //Spawn Down
        GameObject bottomBlock = Instantiate(m_BlockPrefab, m_SpawnPoint.transform.position, Quaternion.identity);
        bottomBlock.name = "BottomBlock";
        bottomBlock.transform.Translate(0.0f, m_BottomEdgeY + BLOCK_YOFFSET, 0.0f);
        BlockController bottomBlockScript = bottomBlock.GetComponent<BlockController>();
        bottomBlockScript.SetSize(bottomHeight);
        m_Blocks.Add(bottomBlockScript);

        //Spawn Gap
        GameObject gap = Instantiate(m_GapPrefab, m_SpawnPoint.transform.position, Quaternion.identity);
        gap.transform.Translate(0.0f, topBlock.transform.position.y - BLOCK_YOFFSET - (gapHeight / 4), 0.0f);
        Gap gapScript = gap.GetComponent<Gap>();
        gapScript.SetSize(gapHeight);
        m_Gaps.Add(gapScript);
        m_Time = Time.time;
    }

    private IEnumerator CheckCollision()
    {
        while(m_IsStart)
        {
            

            for (int i = 0; i < m_Blocks.Count; ++i)
            {
                //TODO: tweak Bird yOffset to suitable number. Need more testing
                float x = Mathf.Pow(m_Blocks[i].transform.position.x - m_Bird.transform.position.x, 2);
                if (x <= 1 && ((m_Bird.transform.position.y + BIRD_YOFFSET) >= (m_Blocks[i].transform.position.y - BLOCK_YOFFSET) && (m_Bird.transform.position.y - BIRD_YOFFSET) <= ((m_Blocks[i].transform.position.y - BLOCK_YOFFSET) + (m_Blocks[i].Height / 2))))
                {
                    Debug.Log("Death - " + m_Blocks[i].name);
                    yield return new WaitForSeconds(0.3f);
                }
            }

            for (int i = 0; i < m_Gaps.Count; ++i)
            {
                float x = Mathf.Pow(m_Gaps[i].transform.position.x - m_Bird.transform.position.x, 2);
                float y = Mathf.Pow(m_Gaps[i].transform.position.y - m_Bird.transform.position.y, 2);

                if (x <= 0.25f && y <= (m_Gaps[i].Height / 4))
                {
                    //Debug.Log("Score - Gap[" + i + "]");
                    m_Score++;
                    m_ScoreText.text = m_Score.ToString();
                    yield return new WaitForSeconds(0.3f);
                }
            }

            yield return new WaitForSeconds(0.3f);
        }


        yield break;
    }

    //private bool CheckCollision(GameObject one, GameObject two) // AABB - AABB collision
    //{
    //    //// collision x-axis?
    //    //bool collisionX = one.transform.position.x + one.x >= two.Position.x &&
    //    //    two.Position.x + two.Size.x >= one.Position.x;
    //    //// collision y-axis?
    //    //bool collisionY = one.Position.y + one.Size.y >= two.Position.y &&
    //    //    two.Position.y + two.Size.y >= one.Position.y;
    //    //// collision only if on both axes
    //    //return collisionX && collisionY;
    //}
}
