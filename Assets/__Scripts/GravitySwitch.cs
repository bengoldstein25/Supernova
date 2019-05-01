using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour {

    public float x;
    public float y;
    public float z;

	void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player") {
            coll.gameObject.GetComponent<Player>().ChangeGravity(new Vector3(x, y, z));
        }
    }
}
