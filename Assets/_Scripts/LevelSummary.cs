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

    public void SetupLevelSummary(Level setupLevel)
    {
        this.associatedLevel = setupLevel;

        this.levelName.text = setupLevel.levelName;
        this.levelImage.sprite = Resources.Load<Sprite>("LevelImages/" + setupLevel.imageFileName);
        this.levelDifficulty.text = setupLevel.difficulty;
        int playerScore = PlayerPrefs.GetInt(setupLevel.sceneName, 0);
        this.playerScore.text = (playerScore == 0) ? "Unbeaten" : playerScore.ToString();
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
