using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float timer = 0.0f;

    private int timeMaxScore = 6666;
    private int challengeTimeMaxScore = 99999;
    private int airMaxScore = 3333;
    private int timeMultiplier = 80;

    private bool levelComplete = false;

    public bool preGameComplete = false;

    private GameObject endScreenPrefab;

    private Coroutine quittingCoroutine = null;

    private GameObject quitCanvasPrefab;
    private GameObject quitCanvas;
    private float fillDelta = 0.02f;

    public TextMeshProUGUI challengeTimerText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this.timer = 0.0f;
        this.endScreenPrefab = Resources.Load<GameObject>("EndScreen");
        this.quitCanvasPrefab = Resources.Load<GameObject>("QuitCanvas");
        this.quitCanvas = GameObject.Instantiate(this.quitCanvasPrefab);
        this.challengeTimerText = GameObject.Find("ChallengeTimer").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (this.quittingCoroutine == null)
            {
                StopAllCoroutines();
                this.quittingCoroutine = StartCoroutine(QuitSequence(KeyCode.Q));
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.quittingCoroutine == null)
            {
                StopAllCoroutines();
                this.quittingCoroutine = StartCoroutine(QuitSequence(KeyCode.Escape));
            }
        }
    }

    private void FixedUpdate()
    {
        if (this.preGameComplete == true && this.levelComplete == false)
        {
            timer += Time.fixedDeltaTime;
            SceneLoader.challengeTimer += Time.fixedDeltaTime;
        }
    }

    public void FinishLevel(float currentAirLevel, float maxAirLevel)
    {
        this.levelComplete = true;

        if (SceneLoader.instance.challengeMode == true)
        {
            this.ChallengeLevelFinished();
            return;
        }

        GameObject.Find("QuitCanvas(Clone)").SetActive(false);

        int timeDeduction = Mathf.RoundToInt(timer * this.timeMultiplier);
        if (timeDeduction < 0)
        {
            timeDeduction = this.timeMaxScore;
        }

        int levelScore = (this.timeMaxScore - timeDeduction) + (int)(this.airMaxScore * (currentAirLevel / 100.0f));

        string levelStats = PlayerPrefs.GetString(SceneManager.GetActiveScene().name, "");
        string[] levelStatsArray = levelStats.Split(',');


        int previousScore = (levelStats == "") ? 0 : Int32.Parse(levelStatsArray[0]);
        if (levelScore > previousScore)
        {
            string prefsString = levelScore.ToString() + "," + this.GetTimerString() + "," + Math.Round(currentAirLevel, 2).ToString() + "%";
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, prefsString);
        }

        this.SetupEndScreen(levelScore, (this.timeMaxScore - timeDeduction), (int)(this.airMaxScore * (currentAirLevel / 100.0f)), currentAirLevel);

        HighScores.UploadScore(PlayerPrefs.GetString("name", "NoName"), levelScore);
        
        LevelManager.instance.CheckGameBeaten();
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

    private void SetupEndScreen(int levelScore, int timeContribution, int airContribution, float currentAirLevel)
    {
        GameObject endScreenObject = GameObject.Instantiate(this.endScreenPrefab);
        EndScreen endScreenComponent = endScreenObject.GetComponent<EndScreen>();
        endScreenComponent.SetupEndScreen(levelScore, timeContribution, airContribution, currentAirLevel);
    }

    IEnumerator QuitSequence(KeyCode pressedKey)
    {
        Image quitImage = this.quitCanvas.GetComponentInChildren<Image>();

        while (Input.GetKey(pressedKey) && quitImage.fillAmount < 1)
        {
            quitImage.fillAmount += this.fillDelta;
            yield return new WaitForFixedUpdate();
        }

        if (Input.GetKey(pressedKey))
        {
            if (SceneLoader.instance.challengeMode == true)
            {
                SceneLoader.instance.LoadScene("ChallengeMode");
            }
            else
            {
                SceneLoader.instance.LoadScene("LevelSelect");
            }
        }
        else
        {
            this.quittingCoroutine = null;
            while (quitImage.fillAmount > 0)
            {
                quitImage.fillAmount -= this.fillDelta;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    private void ChallengeLevelFinished()
    {
        LevelManager.instance.levelIndex++;

        if (LevelManager.instance.levelIndex < LevelManager.instance.levelList.Length)
        {
            SceneLoader.instance.LoadScene(LevelManager.instance.GetCurrentLevel().sceneName);
        }
        else
        {
            //Show endgame screen
            int timeDeduction = Mathf.RoundToInt(SceneLoader.challengeTimer * this.timeMultiplier);
            if (timeDeduction < 0)
            {
                timeDeduction = this.challengeTimeMaxScore;
            }

            int levelScore = (this.challengeTimeMaxScore - timeDeduction);

            string levelStats = PlayerPrefs.GetString("challenge", "");
            string[] levelStatsArray = levelStats.Split(',');


            int previousScore = (levelStats == "") ? 0 : Int32.Parse(levelStatsArray[0]);
            if (levelScore > previousScore)
            {
                string prefsString = levelScore.ToString() + "," + this.GetTimerString() + ",0.0%";
                PlayerPrefs.SetString("challenge", prefsString);
            }

            this.SetupEndScreen(levelScore, (this.challengeTimeMaxScore - timeDeduction), 0, 0);

            HighScores.UploadScore(PlayerPrefs.GetString("name", "NoName"), levelScore);
        }
    }
}
