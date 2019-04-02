using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed = 20;
    public float turningSpeed = 60;
    public GameObject com;

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = com.transform.localPosition;
    }

    void Update() {
        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        if (vertical >= 0) {
            transform.Rotate(0, horizontal, 0);
        } else {
            transform.Rotate(0, -1 * horizontal, 0);
        }

    }
}
