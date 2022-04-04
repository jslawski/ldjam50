using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCard : MonoBehaviour
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

    public void SetupLevelCard(Level setupLevel)
    {
        this.associatedLevel = setupLevel;
        this.levelName.text = setupLevel.levelName;
        this.levelImage.sprite = Resources.Load<Sprite>("LevelImages/" + setupLevel.imageFileName);
        this.levelDifficulty.text = setupLevel.difficulty;

        string levelStats = PlayerPrefs.GetString(setupLevel.sceneName, "");
        string[] levelStatsArray = levelStats.Split(',');
        
        this.playerScore.text = (levelStats == "") ? "Unbeaten" : levelStatsArray[1];

        this.playerName.text = PlayerPrefs.GetString("name", "");
    }

    public void SelectLevel()
    {
        LevelManager.instance.SelectLevel(this.associatedLevel);
    }
}
