using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour {

    [Header("Changes as player does laps")]
    public int num_laps = 1;

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
        elapsedTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (num_laps > total_laps)
        {
            hasWon = true;
        }

        if (!hasWon)
        {
            lapCounter.text = "Lap " + num_laps + " of " + total_laps;
            elapsedTime += Time.deltaTime;
            string min = ((int) elapsedTime / 60).ToString();
            int sec_i = (int)elapsedTime % 60;
            string sec = sec_i.ToString();
            if (sec_i < 10) sec = "0" + sec;


            timeCounter.text = min + ":" + sec;
        }
        else
        {
            lapCounter.text = "You won!";
            timeCounter.color = Color.cyan;
        }
        

    }
    
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            if (!startedRace) startedRace = true;
            else num_laps++;
        }
    }
}
