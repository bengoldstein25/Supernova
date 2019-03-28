using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    public GameObject target;
    public Transform targetTransform;
    public float damping = 6.0f;
    public bool smooth = true;
    public float minDistance = 10.0f;
    public string property = "";
    public Vector3 goalDistance;

    private Color color;
    private float alpha = 1.0f;
    private Transform _myTransform;

    void Awake() {
        _myTransform = transform;
        targetTransform = target.transform;
        goalDistance = this.transform.position - targetTransform.position;
    }
    
    void LateUpdate() {
        this.transform.position = targetTransform.position + goalDistance;
        if (target) {
            if (smooth) {
                Quaternion rotation = Quaternion.LookRotation(targetTransform.position - _myTransform.position);
                _myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, rotation, Time.deltaTime * damping);
            } else {
                _myTransform.rotation = Quaternion.FromToRotation(-Vector3.forward, (new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z) - _myTransform.position).normalized);

                float distance = Vector3.Distance(targetTransform.position, _myTransform.position);

                if (distance < minDistance) {
                    alpha = Mathf.Lerp(alpha, 0.0f, Time.deltaTime * 2.0f);
                } else {
                    alpha = Mathf.Lerp(alpha, 1.0f, Time.deltaTime * 2.0f);

                }
            }
        }
    }
}