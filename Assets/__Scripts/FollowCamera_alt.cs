using UnityEngine;
using System.Collections;

public class FollowCamera_alt : MonoBehaviour {
    public GameObject toFollow;
    public Transform target;
    [Header("For adjusting angle offset")]
    public float x_rot_angle = 0.0f;
    public float y_rot_angle = 0.0f;
    public float z_rot_angle = 0.0f;
    [Header("For adjusting position offset")]
    public float distance = 8.0f;
    public float height = 6.0f;
    public float damping = 5.0f;
    [Header("Rotation/following options")]
    public bool smoothRotation = true;
    public float rotationDamping = 10.0f;
    public bool followBehind = true;

    void Start() {
        this.target = toFollow.transform;
    }

    void Update() {
        Vector3 wantedPosition;
        if (followBehind)
            wantedPosition = target.TransformPoint(0, height, -distance);
        else
            wantedPosition = target.TransformPoint(0, height, distance);

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        Vector3 rot;
        rot.x = x_rot_angle;
        rot.y = y_rot_angle;
        rot.z = z_rot_angle;

        if (smoothRotation) {
            Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            wantedRotation.x += x_rot_angle;
            wantedRotation.y += y_rot_angle;
            wantedRotation.z += z_rot_angle;
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        } else transform.LookAt(target, target.up);
    }
}