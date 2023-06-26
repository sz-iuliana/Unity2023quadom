using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

   

    private  void Show()
    {
        gameObject.SetActive(true);
    }

   
   private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        Hide();
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePause();
        });


        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void KitchenGameManager_OnGamePaused(object sender ,EventArgs e)
    {
        Show();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender ,EventArgs e)
    {
        Hide();
    }

}
