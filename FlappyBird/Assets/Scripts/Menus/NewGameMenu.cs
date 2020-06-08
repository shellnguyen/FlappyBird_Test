using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameMenu : MonoBehaviour
{
    [SerializeField] private Button m_BtnPlay;

    // Start is called before the first frame update
    private void Start()
    {
        m_BtnPlay.onClick.AddListener(OnNewGame);
    }

    private void OnNewGame()
    {
        Utilities.Instance.DispatchEvent(Shell.Event.ShowPopup, "tutorial", false);
        this.gameObject.SetActive(false);
    }
}
