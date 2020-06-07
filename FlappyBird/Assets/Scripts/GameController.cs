using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private const float BLOCK_YOFFSET = 0.5f;
    private const float BIRD_YOFFSET = 0.4f;

    [SerializeField] private GameSetting m_GameSetting;

    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private Vector2 m_ScreenBounds;
    [SerializeField] private bool m_IsStart;

    [SerializeField] private BirdController m_Bird;

    [SerializeField] private GameObject m_SpawnPoint;

    [SerializeField] private List<Section> m_Sections;
    [SerializeField] private GameObject m_SectionPrefab;

    //[SerializeField] private Li
    [SerializeField] private float m_TimeBlock;
    [SerializeField] private float m_TimeCloud;

    [SerializeField] private GameObject m_Floor;

    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [SerializeField] private int m_Score;

    private void OnEnable()
    {
        m_GameSetting = GameSetting.Instance;
        PoolController.Instance.Initialize(m_SpawnPoint.transform.position);
        EventManager.Instance.Register(Shell.Event.OnTutorialEnd, OnNewGame);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Shell.Event.OnTutorialEnd, OnNewGame);
    }

    // Start is called before the first frame update
    private void Start()
    {
        float xHeight = m_Floor.GetComponent<SpriteRenderer>().bounds.extents.y;

        m_ScreenBounds = m_MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, m_MainCamera.transform.position.z));
        m_GameSetting.screenBounds = m_ScreenBounds;
        m_IsStart = false;
        m_Score = 0;
        m_Sections = new List<Section>();

        m_Bird.SetScreenBounds(m_ScreenBounds, xHeight);
    }

    private void FixedUpdate()
    {
        if (!m_IsStart)
        {
            return;
        }

        if (m_Sections.Count < 15 && (Time.time - m_TimeBlock) > 8.0f)
        {
            SpawnBlock();
        }

        if(Time.time - m_TimeCloud > 4.0f)
        {
            SpawnClound();
        }
    }

    private void SpawnBlock()
    {
        //GameObject section = Instantiate(m_SectionPrefab, m_SpawnPoint.transform.position, Quaternion.identity);
        //m_Sections.Add(section.GetComponent<Section>());
        m_Sections.Add(PoolController.Instance.Get("section", m_SpawnPoint.transform.position, Quaternion.identity).GetComponent<Section>());
        m_TimeBlock = Time.time;
    }

    private void SpawnClound()
    {
        IObjectPool cloudScript = PoolController.Instance.Get("cloud", m_SpawnPoint.transform.position, Quaternion.identity).GetComponent<IObjectPool>();
        cloudScript.OnReuse();
        m_TimeCloud = Time.time;
    }

    private IEnumerator CheckCollision()
    {
        while(m_IsStart)
        {
            //Check collide with anything in section
            for(int i = 0; i < m_Sections.Count; ++i)
            {
                if(!m_Sections[i].enabled)
                {
                    continue;
                }

                int result = m_Sections[i].IsCollide(m_Bird.transform);
                if(result == 1)
                {
                    m_Score++;
                    Utilities.Instance.DispatchEvent(Shell.Event.OnUpdateScore, "score", m_Score);
                    break;
                }

                if(result == -1)
                {
                    Debug.Log("death");
                    Utilities.Instance.DispatchEvent(Shell.Event.OnGameOver, "game_over", m_Score);
                    m_Bird.Hit(result);
                    OnGameOver();
                    break;
                }
            }

            //Check Floor
            float distance = Mathf.Pow(m_Floor.transform.position.y - m_Bird.transform.position.y, 2);
            if (distance <= 1.5f)
            {
                Utilities.Instance.DispatchEvent(Shell.Event.OnGameOver, "game_over", m_Score);
                Debug.Log("Drop death");
                m_Bird.Hit(-2);
                OnGameOver();
                //yield return new WaitForSeconds(0.3f);
            }

            yield return new WaitForSeconds(0.3f);
        }


        yield break;
    }

    private void OnNewGame(EventParam param)
    {
        bool isPlayAgain = param.GetBoolean("new_game");

        if(isPlayAgain)
        {

        }

        m_IsStart = true;
        m_Score = 0;
        m_Bird.gameObject.SetActive(true);

        m_TimeBlock = Time.time;
        m_TimeCloud = Time.time;
        StartCoroutine(CheckCollision());
    }

    private void OnGameOver()
    {
        m_IsStart = false;
    }
}
