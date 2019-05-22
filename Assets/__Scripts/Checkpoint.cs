using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player1")
        {
            FinishLine parent = GetComponentInParent<FinishLine>();
            parent.Checkpoint_triggered(gameObject, 1);
        }
        if (coll.tag == "Player2") {
            FinishLine parent = GetComponentInParent<FinishLine>();
            parent.Checkpoint_triggered(gameObject, 2);
        }
    }
}
