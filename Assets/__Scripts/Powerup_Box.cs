using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Box : MonoBehaviour {

	void OnTriggerEnter(Collider coll)
	{
		//See if a player hit the powerup box
		GameObject collidedWith = coll.gameObject;

		if (collidedWith.tag == "Player" || collidedWith.tag == "Player2") {

			//if the powerup box gets collided with, make it disappear for 10 seconds
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<Collider> ().enabled = false;
			//GameObject.active = false;
			Invoke ("Reappear", 5f);
		}

	}

	void Reappear(){
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<Collider> ().enabled = true;
		//GameObject.active = true;
	}
}
