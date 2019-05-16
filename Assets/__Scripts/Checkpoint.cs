using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            FinishLine parent = GetComponentInParent<FinishLine>();
            parent.Checkpoint_triggered(gameObject);
        }   
    }
}
