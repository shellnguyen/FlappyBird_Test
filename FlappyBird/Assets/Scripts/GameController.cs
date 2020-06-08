using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private string SAVE_PATH;
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

    [SerializeField] private float m_TimeBlock;
    [SerializeField] private float m_TimeCloud;

    [SerializeField] private GameObject m_Floor;

    [SerializeField] private int m_Score;

    #region Unity functions
    private void Awake()
    {
        m_GameSetting = GameSetting.Instance;
        SAVE_PATH = Application.persistentDataPath + "/player.sav";
        LoadSetting();
    }

    private void OnEnable()
    {
        PoolController.Instance.Initialize(m_SpawnPoint.transform.position);
        EventManager.Instance.Register(Shell.Event.OnNewGame, OnNewGame);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Shell.Event.OnNewGame, OnNewGame);
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

    private void Update()
    {
        if (!m_IsStart)
        {
            return;
        }

        if ((Time.time - m_TimeBlock) > 8.0f)
        {
            SpawnBlock();
        }

        if(Time.time - m_TimeCloud > 4.0f)
        {
            SpawnClound();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSetting();
    }
    #endregion

    private void SpawnBlock()
    {
        if(m_Sections.Count <= 20)
        {
            m_Sections.Add(PoolController.Instance.Get("section", m_SpawnPoint.transform.position, Quaternion.identity).GetComponent<Section>());
        }
        
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
                if(!m_Sections[i].gameObject.activeSelf)
                {
                    continue;
                }

                int result = m_Sections[i].IsCollide(m_Bird.transform);
                if(result == 1)
                {
                    m_Score++;
                    Utilities.Instance.DispatchEvent(Shell.Event.PlayAudio, "play_one", 3);
                    Utilities.Instance.DispatchEvent(Shell.Event.OnUpdateScore, "score", m_Score);
                    break;
                }

                if(result == -1)
                {
                    Debug.Log("death");
                    m_Bird.Hit(result);
                    OnGameOver();
                    break;
                }
            }

            //Check Floor
            float distance = Mathf.Pow(m_Floor.transform.position.y - m_Bird.transform.position.y, 2);
            if (distance <= 1.5f)
            {
                Debug.Log("Drop death");
                m_Bird.Hit(-2);
                OnGameOver();
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
            OnReset();
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
        Utilities.Instance.DispatchEvent(Shell.Event.PlayAudio, "play_one", 2);
        Utilities.Instance.DispatchEvent(Shell.Event.ShowPopup, "game_over", m_Score);
    }

    private void OnReset()
    {
        m_Bird.OnReset();
        for(int i = 0; i < m_Sections.Count; ++i)
        {
            if(m_Sections[i].gameObject.activeSelf)
            {
                m_Sections[i].gameObject.SetActive(false);
            }
        }
    }

    private bool SaveSetting()
    {
        FileStream fs = new FileStream(SAVE_PATH, FileMode.Create);

        // Construct a BinaryFormatter and use it to serialize the data to the stream.
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            SettingData data = new SettingData();
            data.enableAudio = m_GameSetting.enableAudio;
            data.highScore = m_GameSetting.highScore;
            formatter.Serialize(fs, data);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason: " + e.Message);
            return false;
        }
        finally
        {
            fs.Close();
        }

        return true;
    }

    private bool LoadSetting()
    {
        if (File.Exists(SAVE_PATH))
        {
            FileStream fs = new FileStream(SAVE_PATH, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                SettingData data = (SettingData)formatter.Deserialize(fs);

                m_GameSetting.enableAudio = data.enableAudio;
                m_GameSetting.highScore = data.highScore;
            }
            catch (SerializationException e)
            {
                Debug.Log("Failed to deserialize. Reason: " + e.Message);
                return false;
            }
            finally
            {
                fs.Close();
            }
        }
        else
        {
            Debug.Log("File not found !!!");
            return false;
        }

        return true;
    }
}
