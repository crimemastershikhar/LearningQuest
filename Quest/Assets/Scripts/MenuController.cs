using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Canvas[] canvases;
    [SerializeField] private Button[] chapterBtns;

    private void OnEnable()
    {
        playBtn.onClick.AddListener(ClickPlayScene);

        for (int i = 0; i < chapterBtns.Length; i++)
        {
            int btnIndex = i;
            chapterBtns[i].onClick.AddListener(() => ChooseChapter(btnIndex));
            chapterBtns[i].onClick.AddListener(SwitchToGameScene);
        }
    }

    private void OnDisable()
    {
        playBtn.onClick.RemoveListener(ClickPlayScene);
        
        for (int i = 0; i < chapterBtns.Length; i++)
        {
            int btnIndex = i;
            chapterBtns[i].onClick.RemoveListener(() => ChooseChapter(btnIndex));
            chapterBtns[i].onClick.RemoveListener(SwitchToGameScene);
        }
    }

    private void ClickPlayScene()
    {
        canvases[0].gameObject.SetActive(false);
        canvases[1].gameObject.SetActive(true);
    }

    private void ChooseChapter(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0:
                GitHubUrl.SharedUrl = "https://raw.githubusercontent.com/crimemastershikhar/LearningQuest/refs/heads/MilestoneOne/image_data.json";
                break;
            case 1:
                GitHubUrl.SharedUrl = "https://raw.githubusercontent.com/crimemastershikhar/LearningQuest/refs/heads/MilestoneOne/image_data01.json";
                break;
            case 2:
                GitHubUrl.SharedUrl = "https://raw.githubusercontent.com/crimemastershikhar/LearningQuest/refs/heads/MilestoneOne/image_data02.json";
                break;
            case 3:
                GitHubUrl.SharedUrl = "https://raw.githubusercontent.com/crimemastershikhar/LearningQuest/refs/heads/MilestoneOne/image_data03.json";
                break;
            
            default:
                Debug.LogError("Unknown button index: " + buttonIndex);
                break;
        }
    }

    private void SwitchToGameScene()
    {
        SceneManager.LoadScene((int)SceneNames.Gameplay);
    }
}

public enum SceneNames
{
    MainMenu,
    Gameplay
}

public static class GitHubUrl
{
    public static string SharedUrl { get; set; }
}
