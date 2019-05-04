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

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = com.transform.localPosition;
        rb = GetComponent<Rigidbody>();
        onGround = false;
        rb.drag = friction;
    }

    void Update() {
        
        float vertical = Input.GetAxis("Vertical") * movementSpeed;
        if (onGround) {
            Vector3 forceToAdd = transform.forward * vertical;
            rb.AddForce(accelFactor * forceToAdd, ForceMode.Acceleration);
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
