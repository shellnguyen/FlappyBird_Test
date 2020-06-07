using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //[SerializeField] private
    [SerializeField] private GameObject m_ScorePanel;
    [SerializeField] private GameObject m_TutorialPanel;
    [SerializeField] private GameObject m_GameOverPanel;

    private void OnEnable()
    {
        EventManager.Instance.Register(Shell.Event.OnNewGame, OnNewGame);
        EventManager.Instance.Register(Shell.Event.OnGameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Shell.Event.OnNewGame, OnNewGame);
        EventManager.Instance.Unregister(Shell.Event.OnGameOver, OnGameOver);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnNewGame(EventParam param)
    {
        //Start the tutorial screen
        m_ScorePanel.SetActive(true);
        m_TutorialPanel.SetActive(true);
    }

    private void OnGameOver(EventParam param)
    {
        m_ScorePanel.SetActive(false);
    }
}
