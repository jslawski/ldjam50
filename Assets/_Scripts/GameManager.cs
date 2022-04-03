using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float timer = 0.0f;

    private int maxScore = 9999;
    private int timeMultiplier = 1;
    private int airMultiplier = 10;

    private bool levelComplete = false;

    [SerializeField]
    private GameObject endScreenObject;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        if (this.levelComplete == false)
        {
            timer += Time.fixedDeltaTime;
        }
    }

    public void FinishLevel(float currentAirLevel, float maxAirLevel)
    {
        this.levelComplete = true;

        int timeDeduction = Mathf.RoundToInt(timer * this.timeMultiplier);
        int airDeduction = Mathf.RoundToInt((maxAirLevel - currentAirLevel) * airMultiplier);

        int levelScore = this.maxScore - timeDeduction - airDeduction;

        int previousScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0);
        if (levelScore > previousScore)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, levelScore);
            HighScores.UploadScore(PlayerPrefs.GetString("name", ""), levelScore);
        }
    }

    public List<int> GetTimerValues()
    {
        List<int> timerValues = new List<int>();

        int minutesValue = Mathf.FloorToInt(this.timer / 60f);
        timerValues.Add(minutesValue);

        int secondsValue = Mathf.FloorToInt(this.timer - (minutesValue * 60));
        timerValues.Add(secondsValue);

        int millisecondsValue = (int)((this.timer - (minutesValue * 60) - secondsValue) * 100);
        timerValues.Add(millisecondsValue);

        return timerValues;
    }

    private void SetupEndScreen()
    {
        this.endScreenObject.SetActive(true);


    }
}
