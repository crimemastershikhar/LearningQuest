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

    private void OnEnable()
    {
        playBtn.onClick.AddListener(SwitchToPlayScene);
    }

    private void OnDisable()
    {
        playBtn.onClick.RemoveListener(SwitchToPlayScene);
    }

    private void SwitchToPlayScene()
    {
        SceneManager.LoadScene((int)SceneNames.Gameplay);
    }
}

public enum SceneNames
{
    MainMenu,
    Gameplay
}
