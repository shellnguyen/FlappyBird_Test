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
        EventManager.Instance.Register(Shell.Event.OnUpdateScore, OnScoreUpdate);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unregister(Shell.Event.OnUpdateScore, OnScoreUpdate);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_ResourceManager = ResourcesManager.Instance;
    }

    private void OnScoreUpdate(EventParam param)
    {
        string tag = param.GetString("tag");
        int score = param.GetInt("score");

        int tens = score / 10;
        int unit = score % 10;

        m_TenImage.sprite = m_ResourceManager.scores[tens];
        m_UnitImage.sprite = m_ResourceManager.scores[unit];
    }
}
