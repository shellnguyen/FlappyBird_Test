using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour, IObjectPool
{
    private const float BLOCK_YOFFSET = 0.5f;
    private const float BIRD_YOFFSET = 0.4f;

    [SerializeField] private GameObject m_BlockPrefab;
    [SerializeField] private GameObject m_GapPrefab;

    [SerializeField] private BlockController m_TopBlock;
    [SerializeField] private Gap m_Gap;
    [SerializeField] private BlockController m_BottomBlock;

    [SerializeField] private float m_MoveSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        float topHeight = Random.Range(1, 6) * 2;
        float gapHeight = Random.Range(3, 4) * 2;
        float bottomHeight = 20.0f - topHeight - gapHeight;

        //Spawn Top
        GameObject topBlock = Instantiate(m_BlockPrefab, this.transform);
        topBlock.name = "TopBlock";
        topBlock.transform.Translate(0.0f, GameSetting.Instance.screenBounds.y - (topHeight / 2) + BLOCK_YOFFSET, 0.0f);
        m_TopBlock = topBlock.GetComponent<BlockController>();
        m_TopBlock.SetSize(topHeight);

        //Spawn Bottom
        GameObject bottomBlock = Instantiate(m_BlockPrefab, this.transform);
        bottomBlock.name = "BottomBlock";
        bottomBlock.transform.Translate(0.0f, -GameSetting.Instance.screenBounds.y + BLOCK_YOFFSET, 0.0f);
        m_BottomBlock = bottomBlock.GetComponent<BlockController>();
        m_BottomBlock.SetSize(bottomHeight);

        //Spawn Gap
        GameObject gap = Instantiate(m_GapPrefab, this.transform);
        gap.transform.Translate(0.0f, topBlock.transform.position.y - BLOCK_YOFFSET - (gapHeight / 4), 0.0f);
        m_Gap = gap.GetComponent<Gap>();
        m_Gap.SetSize(gapHeight);
    }

    // Update is called once per frame
    private void Update()
    {
        OnUpdate();
    }

    private void Move()
    {
        transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
    }

    public int IsCollide(Transform bird)
    {
        //Check Top and Bottom Block
        float xOffsetWithTop = Mathf.Pow(m_TopBlock.transform.position.x - bird.position.x, 2);
        if (xOffsetWithTop <= 1 && ((bird.position.y + BIRD_YOFFSET) >= (m_TopBlock.transform.position.y - BLOCK_YOFFSET) && (bird.position.y - BIRD_YOFFSET) <= ((m_TopBlock.transform.position.y - BLOCK_YOFFSET) + (m_TopBlock.Height / 2))))
        {
            Debug.Log("Hit Top");
            return -1;
        }

        float xOffsetWithBottom = Mathf.Pow(m_BottomBlock.transform.position.x - bird.position.x, 2);
        if (xOffsetWithBottom <= 1 && ((bird.position.y + BIRD_YOFFSET) >= (m_BottomBlock.transform.position.y - BLOCK_YOFFSET) && (bird.position.y - BIRD_YOFFSET) <= ((m_BottomBlock.transform.position.y - BLOCK_YOFFSET) + (m_BottomBlock.Height / 2))))
        {
            Debug.Log("Hit Bottom");
            return -1;
        }
        //

        //Check Gap
        float x = Mathf.Pow(m_Gap.transform.position.x - bird.position.x, 2);
        float y = Mathf.Pow(m_Gap.transform.position.y - bird.position.y, 2);

        if (x <= 0.25f && y <= (m_Gap.Height / 4) && !m_Gap.IsHitRecently)
        {
            Debug.Log("Pass gap");
            m_Gap.IsHitRecently = true;
            return 1;
        }
        //

        return 0;
    }

    public void OnUpdate()
    {
        Move();

        if (transform.position.x < -(GameSetting.Instance.screenBounds.x + 1.0f))
        {
            //Debug.Log("Move out of screen");
            m_Gap.IsHitRecently = false;
            gameObject.SetActive(false);
        }
    }

    public void OnReuse()
    {
        
    }
}
