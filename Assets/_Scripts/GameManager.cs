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

    private GameObject endScreenPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this.timer = 0.0f;
        this.endScreenPrefab = Resources.Load<GameObject>("EndScreen");
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
        }

        this.SetupEndScreen(levelScore, timeDeduction, airDeduction, currentAirLevel);

        HighScores.UploadScore(PlayerPrefs.GetString("name", "NoName"), levelScore);
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

    public string GetTimerString()
    {
        List<int> timerValues = GameManager.instance.GetTimerValues();

        string minutesValue = (timerValues[0] > 9) ? timerValues[0].ToString() : "0" + timerValues[0].ToString();
        string secondsValue = (timerValues[1] > 9) ? timerValues[1].ToString() : "0" + timerValues[1].ToString();
        string millisecondsValue = (timerValues[2] > 9) ? timerValues[2].ToString() : "0" + timerValues[2].ToString();

        return minutesValue + ":" + secondsValue + ":" + millisecondsValue;
    }

    private void SetupEndScreen(int levelScore, int timeDeduction, int airDeduction, float currentAirLevel)
    {
        GameObject endScreenObject = GameObject.Instantiate(this.endScreenPrefab);
        EndScreen endScreenComponent = endScreenObject.GetComponent<EndScreen>();
        endScreenComponent.SetupEndScreen(levelScore, timeDeduction, airDeduction, currentAirLevel);
    }
}
