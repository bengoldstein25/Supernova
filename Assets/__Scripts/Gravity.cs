using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public float gravityAmt;

	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0, -gravityAmt, 0);
	}
}
