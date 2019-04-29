using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour {

    [Header("Changes as player does laps")]
    public int num_laps = 0;

    [Header("Depends on the race")]
    public int total_laps = 3;
    public Text lapCounter;
    public Text timeCounter;
    bool startedRace;
    bool hasWon;
    float elapsedTime;

	// Use this for initialization
	void Start () {
        startedRace = false;
        hasWon = false;
        elapsedTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (num_laps >= total_laps)
        {
            hasWon = true;
        }

        if (!hasWon)
        {
            lapCounter.text = "Lap " + num_laps + " of " + total_laps;
            elapsedTime = Time.time - elapsedTime;
            int min = (int) elapsedTime / 60;
            int sec = (int) elapsedTime % 60;
            timeCounter.text = min.ToString() + ":" + sec.ToString("f0");
        }
        else
        {
            lapCounter.text = "You won!";
            timeCounter.color = Color.cyan;
        }
        

    }
    
    void OnTriggerEnter(Collider coll)
    {
        print("colliding with something");
        if (coll.tag == "Player")
        {
            if (!startedRace) startedRace = true;
            else num_laps++;
        }
    }
}
