﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Utilities.Instance.DispatchEvent(Shell.Event.OnTutorialEnd, "tutorial_end", 0);
            gameObject.SetActive(false);
        }
    }
}
