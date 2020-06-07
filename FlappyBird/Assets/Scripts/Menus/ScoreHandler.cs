using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private ResourcesManager m_ResourceManager;
    [SerializeField] private Image m_TenImage;
    [SerializeField] private Image m_UnitImage;

    private void OnEnable()
    {
        m_ResourceManager = ResourcesManager.Instance;
        EventManager.Instance.Register(Shell.Event.OnUpdateScore, OnScoreUpdate);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Shell.Event.OnUpdateScore, OnScoreUpdate);
    }

    private void OnScoreUpdate(EventParam param)
    {
        int score = param.GetInt("score");

        SetScore(score);
    }

    private IEnumerator CountScore(int score)
    {
        int current = 0;
        while(current <= score)
        {
            SetScore(current);
            current++;

            yield return new WaitForSeconds(0.3f);
        }

        yield break;
    }

    public void ShowScore(int score)
    {
        StartCoroutine(CountScore(score));
    }

    public void SetScore(int score)
    {
        int tens = score / 10;
        int unit = score % 10;

        m_TenImage.sprite = m_ResourceManager.scores[tens];
        m_UnitImage.sprite = m_ResourceManager.scores[unit];
    }
}
