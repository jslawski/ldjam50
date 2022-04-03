using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelName;
    [SerializeField]
    private Image levelImage;
    [SerializeField]
    private TextMeshProUGUI levelDifficulty;
    [SerializeField]
    private TextMeshProUGUI playerScore;

    public void SetupLevelCard(Level setupLevel)
    {
        this.levelName.text = setupLevel.levelName;
        this.levelImage.sprite = Resources.Load<Sprite>("LevelImages/" + setupLevel.imageFileName);
        this.levelDifficulty.text = setupLevel.difficulty;
        int playerScore = PlayerPrefs.GetInt(setupLevel.levelName, 0);
        this.playerScore.text = (playerScore == 0) ? "Unbeaten" : playerScore.ToString();
    }
}
