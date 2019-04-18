using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed = 10;
    public float turningSpeed = 60;
    public GameObject com;
    Rigidbody rb;
    bool onGround;

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = com.transform.localPosition;
        rb = GetComponent<Rigidbody>();
        onGround = false;
    }

    void Update() {
        float vertical = Input.GetAxis("Vertical") * movementSpeed;
        if (onGround) {
            rb.AddForce(transform.forward * vertical, ForceMode.Acceleration);
        }

        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        if (vertical >= 0) {
            transform.Rotate(0, horizontal, 0);
        } else {
            transform.Rotate(0, -1 * horizontal, 0);
        }
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.collider.tag == "Ground") {
            onGround = true;
        }
    }

    void OnCollisionExit(Collision coll) {
        if (coll.collider.tag == "Ground") {
            onGround = false;
        }
    }

    void OnCollisionStay(Collision coll) {
        if (coll.collider.tag == "Ground") {
            onGround = true;
        }
    }
}
