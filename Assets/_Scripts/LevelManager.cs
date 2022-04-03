using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Level[] levelList;

    [SerializeField]
    private GameObject levelCardPrefab;

    [SerializeField]
    private RectTransform levelParent;

    [SerializeField]
    private GameObject levelSummaryObject;

    public int levelIndex;

    private const string LevelSelectSceneName = "LevelSelect";
    private const string MainMenuSceneName = "MainMenu";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        levelList = Resources.LoadAll<Level>("Levels");

        this.LoadLevelsIntoScene();
    }

    private void LoadLevelsIntoScene()
    {
        for (int i = 0; i < levelList.Length; i++)
        {
            this.CreateLevelCard(i);
        }
    }

    private void CreateLevelCard(int levelIndex)
    {
        GameObject newLevelCard = GameObject.Instantiate(this.levelCardPrefab, levelParent);
        LevelCard levelCardComponent = newLevelCard.GetComponent<LevelCard>();
        levelCardComponent.SetupLevelCard(levelList[levelIndex]);
    }

    public void SelectLevel(Level selectedLevel)
    {
        LevelSummary levelSummaryComponent = this.levelSummaryObject.GetComponent<LevelSummary>();
        levelSummaryComponent.SetupLevelSummary(selectedLevel);
        this.levelSummaryObject.SetActive(true);
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        this.levelIndex++;
        HighScores.privateCode = this.levelList[this.levelIndex].privateLeaderboardKey;
        HighScores.publicCode = this.levelList[this.levelIndex].publicLeaderboardKey;
        SceneManager.LoadScene(this.levelList[this.levelIndex].sceneName);
    }

    public void ReturnToLevelSelect()
    {
        SceneManager.LoadScene(LevelSelectSceneName);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
