using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI airText;
    [SerializeField]
    private TextMeshProUGUI timerScore;
    [SerializeField]
    private TextMeshProUGUI airScore;
    [SerializeField]
    private TextMeshProUGUI totalScore;
    [SerializeField]
    private TextMeshProUGUI levelName;
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private TextMeshProUGUI personalBest;

    [SerializeField]
    private GameObject nextLevelButton;

    public void SetupEndScreen(int levelScore, int timeContribution, int airContribution, float currentAirLevel)
    {
        if (SceneLoader.instance.challengeMode == true)
        {
            this.SetupChallengeEndScreen(levelScore, timeContribution);
            return;
        }

        this.timerText.text = GameManager.instance.GetTimerString();
        this.airText.text = Math.Round(currentAirLevel, 2).ToString() + "% Remaining";

        this.timerScore.text = timeContribution.ToString();
        this.airScore.text = airContribution.ToString();
        this.totalScore.text = levelScore.ToString();

        string levelStats = PlayerPrefs.GetString(LevelManager.instance.GetCurrentLevel().sceneName, "");
        string[] levelStatsArray = levelStats.Split(',');

        this.levelName.text = LevelManager.instance.GetCurrentLevel().levelName;
        this.playerName.text = PlayerPrefs.GetString("name", "");

        if (levelStatsArray.Length > 0)
        {
            this.personalBest.text = levelStatsArray[0];
        }
        else
        {
            this.personalBest.text = levelScore.ToString();
        }


        if (LevelManager.instance.levelIndex == LevelManager.instance.levelList.Length - 1)
        {
            this.nextLevelButton.SetActive(false);
        }
    }

    private void SetupChallengeEndScreen(int levelScore, int timeContribution)
    {
        this.timerText.text = SceneLoader.instance.GetChallengeTimerString();
        this.airText.text = "";
        this.airScore.text = "";

        this.timerScore.text = timeContribution.ToString();
        this.totalScore.text = levelScore.ToString();

        string levelStats = PlayerPrefs.GetString("challenge", "");
        string[] levelStatsArray = levelStats.Split(',');

        this.levelName.text = "Marathon Challenge";
        this.playerName.text = PlayerPrefs.GetString("name", "");

        if (levelStatsArray.Length > 0)
        {
            this.personalBest.text = levelStatsArray[0];
        }
        else
        {
            this.personalBest.text = levelScore.ToString();
        }

        this.nextLevelButton.SetActive(false);
    }

    public void RetryButtonPressed()
    {
        if (SceneLoader.instance.challengeMode == true)
        {
            LevelManager.instance.LoadLevel(LevelManager.instance.levelList[0].sceneName);
            return;
        }

        LevelManager.instance.ReloadLevel();
    }

    public void NextLevelButtonPressed()
    {
        LevelManager.instance.LoadNextLevel();
    }

    public void LevelSelectButtonPressed()
    {
        SceneLoader.instance.challengeMode = false;

        LevelManager.instance.ReturnToLevelSelect();
    }

    public void QuitToMenuButtonPressed()
    {
        LevelManager.instance.ReturnToMainMenu();
    }
}
