using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button gauntletButton;

    private void Awake()
    {
        int gameBeaten = PlayerPrefs.GetInt("gameBeaten", 0);

        if (gameBeaten != 0)
        {
            this.creditsButton.interactable = true;
            this.gauntletButton.interactable = true;
        }
    }

    public void LevelSelectClicked()
    {
        SceneLoader.instance.LoadScene("LevelSelect");
    }

    public void ExitButtonClicked()
    {
        SceneLoader.instance.QuitGame();
    }

    public void CreditsButtonClicked()
    {
        SceneLoader.instance.LoadScene("EndingClip");
    }
}
