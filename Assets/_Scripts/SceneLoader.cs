using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public string nextSceneName;

    private FadePanelManager fadeManager;

    public bool challengeMode = false;

    public static float challengeTimer = 0.0f;

    [SerializeField]
    private AudioSource menuMusic;

    [SerializeField]
    private AudioSource gameMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "MainMenu" || sceneName == "LevelSelect")
        {
            if (this.menuMusic.isPlaying == false)
            {
                this.menuMusic.Play();
            }

            this.gameMusic.Stop();
        }
        else if (sceneName != "LevelSelect" && sceneName != "ChallengeMode" && 
            sceneName != "LoginScene" && sceneName != "IntroClip" && sceneName != "EndingClip")
        {
            if (this.gameMusic.isPlaying == false)
            {
                this.gameMusic.Play();
            }

            this.menuMusic.Stop();
        }

        this.nextSceneName = sceneName;

        fadeManager = GameObject.Find("FadePanel").GetComponent<FadePanelManager>();
        fadeManager.GetComponent<Image>().enabled = true;
        fadeManager.OnFadeSequenceComplete += this.LoadNextScene;
        fadeManager.FadeToBlack();
    }

    private void LoadNextScene()
    {
        fadeManager.OnFadeSequenceComplete -= LoadNextScene;   
        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        SceneManager.LoadScene(this.nextSceneName);

        while (SceneManager.GetActiveScene().name != this.nextSceneName)
        {
            yield return null;
        }
        
        fadeManager = GameObject.Find("FadePanel").GetComponent<FadePanelManager>();
        fadeManager.GetComponent<Image>().enabled = true;
        fadeManager.FadeFromBlack();
    }

    public void QuitGame()
    {
        fadeManager = GameObject.Find("FadePanel").GetComponent<FadePanelManager>();
        fadeManager.OnFadeSequenceComplete += this.CloseGame;
        fadeManager.FadeToBlack();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.challengeTimerText != null && challengeMode == true)
        {
            GameManager.instance.challengeTimerText.text = this.GetChallengeTimerString();
        }
        else if (GameManager.instance != null && GameManager.instance.challengeTimerText != null)
        {
            GameManager.instance.challengeTimerText.text = "";
        }
    }

    public List<int> GetChallengeTimerValues()
    {
        List<int> timerValues = new List<int>();

        int minutesValue = Mathf.FloorToInt(challengeTimer / 60f);
        timerValues.Add(minutesValue);

        int secondsValue = Mathf.FloorToInt(challengeTimer - (minutesValue * 60));
        timerValues.Add(secondsValue);

        int millisecondsValue = (int)((challengeTimer - (minutesValue * 60) - secondsValue) * 100);
        timerValues.Add(millisecondsValue);

        return timerValues;
    }

    public string GetChallengeTimerString()
    {
        List<int> timerValues = this.GetChallengeTimerValues();

        string minutesValue = (timerValues[0] > 9) ? timerValues[0].ToString() : "0" + timerValues[0].ToString();
        string secondsValue = (timerValues[1] > 9) ? timerValues[1].ToString() : "0" + timerValues[1].ToString();
        string millisecondsValue = (timerValues[2] > 9) ? timerValues[2].ToString() : "0" + timerValues[2].ToString();

        return minutesValue + ":" + secondsValue + ":" + millisecondsValue;
    }
}
