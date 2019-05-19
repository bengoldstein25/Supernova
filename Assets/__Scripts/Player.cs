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
    public int playerNumber = 0;

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = com.transform.localPosition;
        rb = GetComponent<Rigidbody>();
        onGround = true;
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
        if (gravChanged) {
            rb.AddForce(grav, ForceMode.Acceleration);
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

    public void ChangeGravity(Vector3 newGrav, Vector3 newDirection) {
        this.rb.useGravity = false;
        this.gravChanged = true;
        print(rb.useGravity);
        this.grav = newGrav;
        this.followCamera.GetComponent<FollowCamera>().changeUp(Vector3.Scale(new Vector3(-1, -1, -1), newGrav).normalized);
        this.transform.rotation = Quaternion.Euler(newDirection);
        this.rb.velocity = Vector3.zero;
    }
}
