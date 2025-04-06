using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeadingManager : MonoBehaviour
{
    [SerializeField] private Button[] headingButtons;
    [SerializeField] private GitHubImageLoader gitHubImageLoader;
    
    [SerializeField] private TextMeshProUGUI descriptionTextLabel;
    [SerializeField] private Button backButton;
    [SerializeField] private AudioSource audioSource;
    

    private void OnEnable()
    {
        for (int i = 0; i < headingButtons.Length; i++)
        {
            if (headingButtons[i] == null) continue;
            int buttonIndex = i;
            headingButtons[i].onClick.AddListener(() => EnableDescription(buttonIndex));
        }
        backButton.onClick.AddListener(SwitchBack);
    }

    private void OnDisable()
    {
        for (int i = 0; i < headingButtons.Length; i++)
        {
            if (headingButtons[i] == null) continue;
            int buttonIndex = i;
            headingButtons[i].onClick.RemoveListener(() => EnableDescription(buttonIndex));
        }
        backButton.onClick.RemoveListener(SwitchBack);
    }

    private void SwitchBack()
    {
        audioSource.Play();
        SceneManager.LoadScene((int)SceneNames.MainMenu);
    }

    private void EnableDescription(int btnIndex)
    {
        audioSource.Play();
        switch (btnIndex)
        {
            case 0:
                descriptionTextLabel.text = gitHubImageLoader.descriptionDisplays[0].text;
                break;
            
            case 1:
                descriptionTextLabel.text = gitHubImageLoader.descriptionDisplays[1].text;
                break;
            
            case 2:
                descriptionTextLabel.text = gitHubImageLoader.descriptionDisplays[2].text;
                break;
            
            case 3:
                descriptionTextLabel.text = gitHubImageLoader.descriptionDisplays[3].text;
                break;
            
            default:
                Debug.LogError("Unknown button index: " + btnIndex);
                break;
        }
    }
}
