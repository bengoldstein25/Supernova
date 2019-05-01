using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed = 10;
    public float turningSpeed = 60;
    public int accelFactor = 1;
    public GameObject com;
    Rigidbody rb;
    GameObject minimapMarker;
    public GameObject followCamera;
    bool onGround;
    private Vector3 grav;
    private bool gravChanged;

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = com.transform.localPosition;
        rb = GetComponent<Rigidbody>();
        onGround = false;
    }

    void FixedUpdate() {
        float vertical = Input.GetAxis("Vertical") * movementSpeed;
        if (onGround) {
            Vector3 forceToAdd = transform.forward * vertical;
            var localVelocity = transform.InverseTransformDirection(rb.velocity);
            var forwardSpeed = localVelocity.z;
            if ((vertical < 0 && forwardSpeed > 0) || (vertical > 0 && forwardSpeed < 0)) {
                forceToAdd = forceToAdd * 2;
            }
            rb.AddForce(accelFactor * forceToAdd, ForceMode.Acceleration);
        }

        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        if (vertical >= 0) {
            transform.Rotate(0, horizontal, 0);
        } else {
            transform.Rotate(0, -1 * horizontal, 0);
        }

        if (gravChanged) {
            rb.AddForce(grav * rb.mass);
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

    public void ChangeGravity(Vector3 newGrav) {
        this.rb.useGravity = false;
        this.gravChanged = true;
        this.grav = newGrav;
        this.followCamera.GetComponent<FollowCamera>().changeUp(Vector3.Scale(new Vector3(-1, -1, -1), newGrav).normalized);
    }
}
