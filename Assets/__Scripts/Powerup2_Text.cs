using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup2_Text : MonoBehaviour {

	public Text powerupText2 = null;
	public Player_alt player2;

	// Update is called once per frame
	void Update () {
		int item2 = player2.returnPlayer2Item ();
		if (item2 == 1) {
			powerupText2.text = "Powerup available: SPEED BOOST";
		} else if (item2 == 2) {
			powerupText2.text = "Powerup available: TIME WARP";
		} else if (item2 == 3) {
			powerupText2.text = "Powerup available: ASTEROID";
		} else if (item2 == 4) {
			powerupText2.text = "Powerup available: NEWTON REGULATOR";
		} else if (item2 == 5) {
			powerupText2.text = "Powerup available: HIJACK";
		} else {
			powerupText2.text = "Powerup available: NONE";
		}
	}
}
