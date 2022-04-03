using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Level[] levelList;

    [SerializeField]
    private GameObject levelCardPrefab;

    [SerializeField]
    private RectTransform levelParent;

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

}
