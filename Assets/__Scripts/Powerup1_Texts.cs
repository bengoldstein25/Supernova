using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup1_Texts : MonoBehaviour {

		public Text powerupText1 = null;
		public Player_alt player1;

		void Update () {
			int item1 = player1.returnPlayer1Item ();
			if (item1 == 1) {
				powerupText1.text = "Powerup available: SPEED BOOST";
			} else if (item1 == 2) {
				powerupText1.text = "Powerup available: TIME WARP";
			} else if (item1 == 3) {
				powerupText1.text = "Powerup available: ASTEROID";
			} else if (item1 == 4) {
				powerupText1.text = "Powerup available: NEWTON REGULATOR";
			} else if (item1 == 5) {
				powerupText1.text = "Powerup available: HIJACK";
			} else {
				powerupText1.text = "Powerup available: NONE";
			}
		}

	}

