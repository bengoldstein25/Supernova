using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour {

    public Vector3 grav;
    public Vector3 newDirection;

	void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player1" || coll.tag == "Player2") {
            coll.gameObject.GetComponent<Player_alt>().ChangeGravity(grav, newDirection);
        }
    }
}
