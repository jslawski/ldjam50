using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSummary : MonoBehaviour
{
    private Level associatedLevel;

    [SerializeField]
    private TextMeshProUGUI levelName;
    [SerializeField]
    private Image levelImage;
    [SerializeField]
    private TextMeshProUGUI levelDifficulty;
    [SerializeField]
    private TextMeshProUGUI playerScore;
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private TextMeshProUGUI playerTime;
    [SerializeField]
    private TextMeshProUGUI playerAir;

    public void SetupLevelSummary(Level setupLevel)
    {
        this.associatedLevel = setupLevel;

        this.levelName.text = setupLevel.levelName;
        this.levelImage.sprite = Resources.Load<Sprite>("LevelImages/" + setupLevel.imageFileName);
        this.levelDifficulty.text = setupLevel.difficulty;

        string levelStats = PlayerPrefs.GetString(setupLevel.sceneName, "");
        string[] levelStatsArray = levelStats.Split(',');

        this.playerScore.text = (levelStats == "") ? "Unbeaten" : levelStatsArray[0];

        this.playerName.text = PlayerPrefs.GetString("name", "");

        if (levelStats != "")
        {
            this.playerTime.text = levelStatsArray[1];
            this.playerAir.text = levelStatsArray[2];
        }
        else
        {
            this.playerTime.text = "";
            this.playerAir.text = "";
        }

        HighScores.privateCode = setupLevel.privateLeaderboardKey;
        HighScores.publicCode = setupLevel.publicLeaderboardKey;
    }

    public void PlayButtonPressed()
    {
        LevelManager.instance.levelIndex = this.associatedLevel.levelIndex;
        LevelManager.instance.LoadLevel(this.associatedLevel.sceneName);
    }

    public void BackButtonPressed()
    {
        this.gameObject.SetActive(false);
    }
}
