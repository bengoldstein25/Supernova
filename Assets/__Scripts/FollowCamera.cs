﻿using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    public GameObject toFollow;
    public Transform target;
    public float distance = 8.0f;
    public float height = 6.0f;
    public float damping = 5.0f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 10.0f;
    Vector3 up;

    void Start() {
        this.target = toFollow.transform;
        this.up = Vector3.up;
    }

    void Update() {
        Vector3 wantedPosition;
        if (followBehind)
            wantedPosition = target.TransformPoint(0, height, -distance);
        else
            wantedPosition = target.TransformPoint(0, height, distance);

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        if (smoothRotation) {
            Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        } else transform.LookAt(target, target.up);
    }

    public void changeUp(Vector3 newUp) {
        this.up = newUp;
    }
}