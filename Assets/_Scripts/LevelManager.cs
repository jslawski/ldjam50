using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
