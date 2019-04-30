using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour {

	void OnCollisionEnter(Collision coll) {
        if(coll.collider.tag == "Player") {
            Physics.gravity = new Vector3(-9.8f, 0, 0);
        }
    }
}
