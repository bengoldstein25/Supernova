using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    RectTransform rt;

	// Use this for initialization
	void Start () {
        this.rt = this.GetComponent<RectTransform>();
        if (LoadingScreen.IsSinglePlayer) {
            rt.anchorMin = new Vector2(1f, 1f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchoredPosition = new Vector2(-100, -100);
        } else {
            rt.anchorMin = new Vector2(0.5f, 1f);
            rt.anchorMax = new Vector2(0.5f, 1f);
            rt.anchoredPosition = new Vector2(0, -100);
        }
	}
}
