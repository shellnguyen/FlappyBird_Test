using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private ScoreHandler m_PlayerScore;
    [SerializeField] private ScoreHandler m_HighScore;
    [SerializeField] private Button m_BtnPlay;
    [SerializeField] private RawImage m_NewImage;

    private void OnEnable()
    {
        m_BtnPlay.onClick.AddListener(OnNewGame);
    }

    public void SetData(int score)
    {
        int highScore = GameSetting.Instance.highScore;

        if(highScore < score)
        {
            highScore = score;
            GameSetting.Instance.highScore = score;
            m_NewImage.gameObject.SetActive(true);
        }
        else
        {
            m_NewImage.gameObject.SetActive(false);
        }

        m_PlayerScore.ShowScore(score);
        m_HighScore.SetScore(highScore);
    }

    private void OnNewGame()
    {
        Utilities.Instance.DispatchEvent(Shell.Event.OnNewGame, "new_game", true);
        this.gameObject.SetActive(false);
    }
}
