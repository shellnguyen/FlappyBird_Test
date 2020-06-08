using UnityEngine;

public class MenuController : MonoBehaviour
{
    //[SerializeField] private
    [SerializeField] private ScoreHandler m_ScorePanel;
    [SerializeField] private GameObject m_TutorialPanel;
    [SerializeField] private GameOverMenu m_GameOverPanel;

    private void OnEnable()
    {
        EventManager.Instance.Register(Shell.Event.ShowPopup, ShowPopup);
        EventManager.Instance.Register(Shell.Event.OnNewGame, OnNewGame);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Shell.Event.ShowPopup, ShowPopup);
        EventManager.Instance.Unregister(Shell.Event.OnNewGame, OnNewGame);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void ShowPopup(EventParam param)
    {
        string tag = param.GetString("tag");

        switch(tag)
        {
            case "game_over":
                {
                    int score = param.GetInt(tag);
                    OnGameOver(score);
                    break;
                }
            case "tutorial":
                {
                    m_ScorePanel.gameObject.SetActive(true);
                    m_TutorialPanel.SetActive(true);
                    break;
                }
        }
    }

    private void OnGameOver(int score)
    {
        m_ScorePanel.gameObject.SetActive(false);
        m_GameOverPanel.gameObject.SetActive(true);
        m_GameOverPanel.SetData(score);
    }

    private void OnNewGame(EventParam param)
    {
        m_ScorePanel.gameObject.SetActive(true);
        m_ScorePanel.SetScore(0);
    }
}
