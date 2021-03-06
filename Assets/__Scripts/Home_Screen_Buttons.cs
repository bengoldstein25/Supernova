﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home_Screen_Buttons : MonoBehaviour {

    public Button play;
    public Button changeCharacter;
    public Button store;
    public Button settings;
    public Button exit;
    public Image toFade;
    private bool switchingScreen;
    private string toLoad;
    private bool isSinglePlayer;

	// Use this for initialization
	void Start () {
        play.onClick.AddListener(PlayOnClick);
        changeCharacter.onClick. AddListener(ChangeCharacterOnClick);
        store.onClick.AddListener(StoreOnClick);
        settings.onClick.AddListener(SettingsOnClick);
        exit.onClick.AddListener(ExitOnClick);
        switchingScreen = false;
	}

    void PlayOnClick () {
        switchingScreen = true;
        toLoad = "Num_Players_Select";
        isSinglePlayer = true;
    }

    void ChangeCharacterOnClick() {
        switchingScreen = true;
        toLoad = "Change_Character";
        isSinglePlayer = true;
    }

    void StoreOnClick() {
        switchingScreen = true;
        toLoad = "Store";
        isSinglePlayer = true;
    }

    void SettingsOnClick() {
        switchingScreen = true;
        toLoad = "Settings";
        isSinglePlayer = true;
    }

    void ExitOnClick() {
        Application.Quit();
    }

    void Update () {
        if (switchingScreen) {
            Color oldColor = toFade.color;
            float oldAlpha = oldColor.a;
            float newAlpha = oldAlpha + 0.01f;
            Color newColor = oldColor;
            newColor.a = newAlpha;
            toFade.color = newColor;
            if(newAlpha >= 1f) {
                switchingScreen = false;
                PlayDoneFading(toLoad);
            }
        }
    }

    void PlayDoneFading(string sceneToLoad) {
        LoadingScreen.To = sceneToLoad;
        LoadingScreen.From = "HomeScreen";
        LoadingScreen.IsSinglePlayer = isSinglePlayer;
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
