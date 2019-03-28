using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_Screen_Starfield : MonoBehaviour {

    public GameObject[] panels;
    public float scrollSpeed = 3f;

    private float panelWidth;
    private float depth;

	// Use this for initialization
	void Start () {
        panelWidth = panels[0].transform.localScale.x;
        depth = panels[0].transform.position.z;

        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(panelWidth, 0, depth);
    }

    // Update is called once per frame
    void Update () {
        float tX = Time.time * scrollSpeed % panelWidth + (panelWidth * 0.5f);
        panels[0].transform.position = new Vector3(tX, 0, depth);
        if(tX > 0) {
            panels[1].transform.position = new Vector3(tX - panelWidth, 0, depth);
        } else {
            panels[1].transform.position = new Vector3(tX + panelWidth, 0, depth);
        }
	}
}
