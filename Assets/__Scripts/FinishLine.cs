using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {

    [Header("Changes as player does laps")]
    public int num_laps_p1 = 1;
    public int num_laps_p2 = 1;


    [Header("Depends on the race")]
    public int total_laps = 3;
    public int num_checkpoints = 3;
    public Text lapCounterP1;
    public Text lapCounterP2;
    public Text timeCounter;
    bool startedRace; //refers to state of the game
    bool hasWon; // refers to state of the game
    int winner;
    float elapsedTime;
    public List<GameObject> cp = new List<GameObject>();
    protected List<int> passed_checkpoints_1 = new List<int>();
    protected List<int> passed_checkpoints_2 = new List<int>();
    float timeToCount = -1f;
    public Image toFade;
    private bool switchingScreen;
    private string toLoad;

    void Start () {
        startedRace = false;
        hasWon = false;
        elapsedTime = 0.0f;
        for (int i = 0; i < num_checkpoints; i++)
        {
            passed_checkpoints_1.Insert(i, 0);
            passed_checkpoints_2.Insert(i, 0);
        }
        winner = 0;
    }

    void Update () {
        if (Input.GetKey("escape")) {
            switchingScreen = true;
            toLoad = "Track_Selection";
        }
        if(timeToCount > 0f) {
            timeToCount -= Time.deltaTime;
        }

        if(hasWon && timeToCount <= 0f) {
            switchingScreen = true;
            toLoad = "Track_Selection";
        }

        if (switchingScreen) {
            Color oldColor = toFade.color;
            float oldAlpha = oldColor.a;
            float newAlpha = oldAlpha + 0.01f;
            Color newColor = oldColor;
            newColor.a = newAlpha;
            toFade.color = newColor;
            if (newAlpha >= 1f) {
                switchingScreen = false;
                PlayDoneFading(toLoad);
            }
        }
        if (num_laps_p1 > total_laps && !hasWon) {
            hasWon = true;
            winner = 1;
        }
        if (num_laps_p2 > total_laps && !hasWon) {
            hasWon = true;
            winner = 2;
        }

        Completed_lap(1);
        Completed_lap(2);

        if (!hasWon)
        {
            lapCounterP1.text = "Lap " + num_laps_p1 + " of " + total_laps;
            lapCounterP2.text = "Lap " + num_laps_p2 + " of " + total_laps;
            elapsedTime += Time.deltaTime;
            string min = ((int) elapsedTime / 60).ToString();
            int sec_i = (int)elapsedTime % 60;
            string sec = sec_i.ToString();
            if (sec_i < 10) sec = "0" + sec;

            timeCounter.text = min + ":" + sec;
        }
        else
        {
            if (winner == 1) {
                lapCounterP1.text = "You won!";
            } else if (winner == 2) {
                lapCounterP2.text = "You won!";
            }
            timeCounter.color = Color.cyan;
            if (timeToCount < 0f) {
                timeToCount = 5;
            }
        }
        

    }
    
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player1")
        {
            if (!startedRace) startedRace = true;
            else
            {
                if (Completed_lap(1))
                {
                    num_laps_p1++;
                    Reset_lap(1);
                } 
            }
        }
        if (coll.tag == "Player2") {
            if (!startedRace) startedRace = true;
            else {
                if (Completed_lap(2)) {
                    num_laps_p2++;
                    Reset_lap(2);
                }
            }
        }
    }
    

    public void Checkpoint_triggered(GameObject go, int carNum)
    {
        int i = cp.IndexOf(go);
        if (i > -1)
        {
            //print(go.name);
            //print(i);
            if (carNum == 1) {
                passed_checkpoints_1.Insert(i, 1);
            } else if (carNum == 2) {
                passed_checkpoints_2.Insert(i, 1);
            }
        }
    }

    private bool Completed_lap(int toCheck)
    {
        bool completed = true;
        if (toCheck == 1) {
            for (int c = 0; c < num_checkpoints; c++) {
                if (passed_checkpoints_1[c] == 0) completed = false;
            }
        } else if (toCheck == 2) {
            for (int c = 0; c < num_checkpoints; c++) {
                if (passed_checkpoints_2[c] == 0) completed = false;
            }
        }
        return completed;
    }

    private void Reset_lap(int toReset)
    {
        if (toReset == 1) {
            for (int i = 0; i < num_checkpoints; i++) {
                passed_checkpoints_1.Insert(i, 0);
            }
        } else if (toReset == 2) {
            for (int i = 0; i < num_checkpoints; i++) {
                passed_checkpoints_2.Insert(i, 0);
            }
        }
    }

    void PlayDoneFading(string sceneToLoad) {
        LoadingScreen.To = sceneToLoad;
        LoadingScreen.From = "FinishLine";
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
