using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Track_Selection : MonoBehaviour {

    public Button m1;
    public Button m2;
    public Button m3;
    public Button m4;
    public Button m5;
    public Button m6;
    public Button exit;
    public Image toFade;
    private bool switchingScreen;
    private string toLoad;

    // Use this for initialization
    void Start() {
        m1.onClick.AddListener(M1OnClick);
        m2.onClick.AddListener(M2OnClick);
        m3.onClick.AddListener(M3OnClick);
        m4.onClick.AddListener(M4OnClick);
        m5.onClick.AddListener(M5OnClick);
        m6.onClick.AddListener(M6OnClick);
        exit.onClick.AddListener(ExitOnClick);
        switchingScreen = false;
    }

    void M1OnClick() {
        switchingScreen = true;
        toLoad = "Squiggly_Square";
    }

    void M2OnClick() {
        switchingScreen = true;
        toLoad = "Lonely_Lagoon";
    }

    void M3OnClick() {
        switchingScreen = true;
        toLoad = "Lunar_Landing";
    }

    void M4OnClick() {
        switchingScreen = true;
        toLoad = "Flaming_Flume";
    }

    void M5OnClick() {
        switchingScreen = true;
        toLoad = "Prehistoric_Parkway";
    }

    void M6OnClick() {
        switchingScreen = true;
        toLoad = "Volatile_Avenue";
    }

    void ExitOnClick() {
        switchingScreen = true;
        toLoad = "Num_Players_Select";
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
        LoadingScreen.From = "Track Selection";
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
