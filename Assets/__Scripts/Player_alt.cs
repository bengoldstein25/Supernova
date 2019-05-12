using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_alt : MonoBehaviour {
    public float movementSpeed = 30;
    public float turningSpeed = 65;
    public float accelFactor = 1;
    public float friction = 0.75f;
    public GameObject com; // center of mass
    Rigidbody rb;
    GameObject minimapMarker;
    bool onGround;
    public int playerNumber = 0;

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = com.transform.localPosition;
        rb = GetComponent<Rigidbody>();
        onGround = false;
        rb.drag = friction;
        if(playerNumber == 0) {
            playerNumber = 1;
        }
    }

    void Update() {
        float vertical = movementSpeed;
        if (playerNumber == 1) {
            vertical = vertical * Input.GetAxis("Vertical-P1");
        } else {
            vertical = vertical * Input.GetAxis("Vertical-P2");
        }
        if (onGround) {
            Vector3 forceToAdd = transform.forward * vertical;
            rb.AddForce(accelFactor * forceToAdd, ForceMode.Acceleration);
        }
        float horizontal = turningSpeed * Time.deltaTime;
        if (playerNumber == 1) {
            horizontal = horizontal * Input.GetAxis("Horizontal-P1");
        } else {
            horizontal = horizontal * Input.GetAxis("Horizontal-P2");
        }
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
