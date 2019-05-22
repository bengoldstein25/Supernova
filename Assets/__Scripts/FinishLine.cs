using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {

    [Header("Changes as player does laps")]
    public int num_laps = 1;

    [Header("Depends on the race")]
    public int total_laps = 3;
    public int num_checkpoints = 3;
    public Text lapCounter;
    public Text timeCounter;
    bool startedRace; //refers to state of the game
    bool hasWon; // refers to state of the game
    float elapsedTime;
    public List<GameObject> cp = new List<GameObject>();
    protected List<int> passed_checkpoints = new List<int>();
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
            passed_checkpoints.Insert(i,0);
        }
    }

    void Update () {
        if (Input.GetKey("escape")) {
            switchingScreen = true;
            toLoad = "Track_Selection";
        }
        if(timeToCount > 0f) {
            timeToCount -= Time.deltaTime;
            print(timeToCount);
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
        if (num_laps > total_laps) hasWon = true;

        Completed_lap();

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
            if (timeToCount < 0f) {
                timeToCount = 5;
            }
        }
        

    }
    
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            if (!startedRace) startedRace = true;
            else
            {
                if (Completed_lap())
                {
                    num_laps++;
                    Reset_lap();
                }
            }
        }
    }
    

    public void Checkpoint_triggered(GameObject go)
    {
        int i = cp.IndexOf(go);
        if (i > -1)
        {
            //print(go.name);
            //print(i);
            passed_checkpoints.Insert(i, 1);
        }
    }

    private bool Completed_lap()
    {
        bool completed = true;
        for (int c  = 0; c < num_checkpoints; c++)
        {
            if (passed_checkpoints[c] == 0) completed = false;
        }
        return completed;
    }

    private void Reset_lap()
    {
        for (int i = 0; i < num_checkpoints; i++)
        {
            passed_checkpoints.Insert(i,0);
        }
    }

    void PlayDoneFading(string sceneToLoad) {
        LoadingScreen.To = sceneToLoad;
        LoadingScreen.From = "FinishLine";
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
