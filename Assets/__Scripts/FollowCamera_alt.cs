using UnityEngine;
using System.Collections;

public class FollowCamera_alt : MonoBehaviour {
    public GameObject toFollow;
    public Transform target;
    private Camera cam;

    public int playerNumber;
    //Quaternion rot;
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
    Vector3 up;

    void Start() {
        this.target = toFollow.transform;
        this.cam = GetComponent<Camera>();
        this.up = Vector3.up;
        if (LoadingScreen.IsSinglePlayer) {
            if (playerNumber == 1) {
                cam.rect = new Rect(0f, 0f, 1f, 1f);
            } else {
                Destroy(toFollow);
                Destroy(gameObject);
            }
        } else {
            if (playerNumber == 1) {
                cam.rect = new Rect(0, 0, 0.5f, 1);
            } else {
                cam.rect = new Rect(0.5f, 0, 1f, 1);
            }
        }

    }

    void Update() {
        Vector3 wantedPosition;
        if (followBehind)
            wantedPosition = target.TransformPoint(0, height, -distance);
        else
            wantedPosition = target.TransformPoint(0, height, distance);

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
        /*
        Vector3 rot;
        rot.x = x_rot_angle;
        rot.y = y_rot_angle;
        rot.z = z_rot_angle;
        */
        if (smoothRotation) {
            Vector3 newRot = target.position - transform.position;
            //newRot.z += z_rot_angle;
            // Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            Quaternion wantedRotation = Quaternion.LookRotation(newRot, up);
            // ================== NEW CODE 
            /*
            if (wantedRotation.x - rot.x < 45 || (rot.x - wantedRotation.x) < 45)
            {
                wantedRotation.x = transform.rotation.x;
            }

            if (wantedRotation.y - rot.y < 45 || rot.y - wantedRotation.y < 45)
            {
                wantedRotation.y = transform.rotation.y;
            }

            if (wantedRotation.y - rot.y < 45 || rot.y - wantedRotation.y < 45)
            {
                wantedRotation.y = transform.rotation.y;
            }
            */
            // ===========================

            /*
            if (wantedRotation.x != 0.0f)
            {
            wantedRotation.x -= x_rot_angle;
            }
            if (wantedRotation.y != 0.0f)
            {
                wantedRotation.y = y_rot_angle;
            }
            if (wantedRotation.z != 0.0f)
            {*/
            wantedRotation.z += z_rot_angle;
            //}

            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        } else transform.LookAt(target, target.up);
    }

    public void changeUp(Vector3 newUp) {
        this.up = newUp;
    }
}