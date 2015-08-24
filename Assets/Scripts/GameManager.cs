using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UiImage = UnityEngine.UI.Image;


public class GameManager : MonoBehaviour {

    private const float TIME_OF_ROUND = 60.0f;

    private float time = TIME_OF_ROUND;

    PlayerController player;

    public GameObject end;
    public Text timeRemaining;
    public Text score;
    public Text finalScore;


	void Start ()
    {
        if (Application.loadedLevel == 1)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
	}


	void Update () 
	{
        if (Application.loadedLevel == 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                StartNewRound();
            }
        }
        if (Application.loadedLevel == 1)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                EndRound();
            }
        }

	}

    public void EndRound ()
    {
        Time.timeScale = 0.0f;
        end.SetActive(true);
        finalScore.text = "Score: " + player.GetScore() + "\nClick to end!";
        if (Input.GetButtonDown("Fire1"))
        {
            Application.LoadLevel("Splash");
        }
    }

    public void StartNewRound ()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel("Game");
    }

    void OnGUI ()
    {
        if (Application.loadedLevel == 1)
        {
            score.text = "Score: " + player.GetScore();
            timeRemaining.text = "Time remaining: " + FormatTime(time);
        }
    }

    string FormatTime (float time)
    {
        int d = (int)(time * 100.0f);
        int minutes = d / (60 * 100);
        int seconds = (d % (60 * 100)) / 100;
        int hundredths = d % 100;
        if (minutes > 0)
        {
            return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
        }
        else
        {
            return string.Format("{0:00}.{1:00}", seconds, hundredths);
        }
    }

}
