using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home_Screen_Buttons : MonoBehaviour {

    public Button play;
    public Image toFade;
    private bool switchingScreen;

	// Use this for initialization
	void Start () {
        play.onClick.AddListener(PlayOnClick);
        switchingScreen = false;
	}

    void PlayOnClick () {
        switchingScreen = true;
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
                PlayDoneFading();
            }
        }
    }

    void PlayDoneFading() {
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
}
