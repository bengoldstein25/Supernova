using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Num_Players_Select : MonoBehaviour {

    public Button player1;
    public Button player2;
    public Button exit;
    public Image toFade;
    private bool switchingScreen;
    private string toLoad;
    private bool isSinglePlayer;

    // Use this for initialization
    void Start() {
        player1.onClick.AddListener(P1OnClick);
        player2.onClick.AddListener(P2OnClick);
        exit.onClick.AddListener(ExitOnClick);
        switchingScreen = false;
    }

    void P1OnClick() {
        switchingScreen = true;
        toLoad = "Track_Selection";
        isSinglePlayer = true;
    }

    void P2OnClick() {
        switchingScreen = true;
        toLoad = "Track_Selection";
        isSinglePlayer = false;
    }

    void ExitOnClick() {
        switchingScreen = true;
        toLoad = "HomeScreen";
        isSinglePlayer = true;
    }

    void Update() {
        if (switchingScreen) {
            Color oldColor = toFade.color;
            float oldAlpha = oldColor.a;
            float newAlpha = oldAlpha + 0.01f;
            Color newColor = oldColor;
            newColor.a = newAlpha;
            toFade.color = newColor;
            if (newAlpha >= 1f) {
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