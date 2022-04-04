using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float timer = 0.0f;

    private int timeMaxScore = 6666;
    private int airMaxScore = 3333;
    private int timeMultiplier = 80;

    private bool levelComplete = false;

    public bool preGameComplete = false;

    private GameObject endScreenPrefab;

    private Coroutine quittingCoroutine = null;

    private GameObject quitCanvasPrefab;
    private GameObject quitCanvas;
    private float fillDelta = 0.02f;

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
        }
    }

    public void FinishLevel(float currentAirLevel, float maxAirLevel)
    {
        this.levelComplete = true;

        int timeDeduction = Mathf.RoundToInt(timer * this.timeMultiplier);
        if (timeDeduction < 0)
        {
            timeDeduction = this.timeMaxScore;
        }

        int levelScore = (this.timeMaxScore - timeDeduction) + (int)(this.airMaxScore * (currentAirLevel / 100.0f));

        int previousScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0);
        if (levelScore > previousScore)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, levelScore);
        }

        this.SetupEndScreen(levelScore, (this.timeMaxScore - timeDeduction), (int)(this.airMaxScore * (currentAirLevel / 100.0f)), currentAirLevel);

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
            SceneManager.LoadScene("LevelSelect");
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
}
