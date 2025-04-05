using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeadingManager : MonoBehaviour
{
    [SerializeField] private Button[] headingButtons;
    [SerializeField] private GitHubImageLoader gitHubImageLoader;

    private void OnEnable()
    {
        for (int i = 0; i < headingButtons.Length; i++)
        {
            if (headingButtons[i] == null) continue;
            int buttonIndex = i;
            headingButtons[i].onClick.AddListener(() => EnableDescription(buttonIndex));
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < headingButtons.Length; i++)
        {
            if (headingButtons[i] == null) continue;
            int buttonIndex = i;
            headingButtons[i].onClick.RemoveListener(() => EnableDescription(buttonIndex));
        }
    }

    private void EnableDescription(int btnIndex)
    {
        switch (btnIndex)
        {
            case 0:
                Debug.Log(gitHubImageLoader.descriptionDisplays[0].text);
                break;
            
            case 1:
                Debug.Log(gitHubImageLoader.descriptionDisplays[1].text);
                break;
            
            case 2:
                Debug.Log(gitHubImageLoader.descriptionDisplays[2].text);
                break;
            
            case 3:
                Debug.Log(gitHubImageLoader.descriptionDisplays[3].text);
                break;
            
            default:
                Debug.LogError("Unknown button index: " + btnIndex);
                break;
        }
    }
}
