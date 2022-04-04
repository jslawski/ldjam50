using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public string nextSceneName;

    private FadePanelManager fadeManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadScene(string sceneName)
    {
        this.nextSceneName = sceneName;

        fadeManager = GameObject.Find("FadePanel").GetComponent<FadePanelManager>();
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
        fadeManager.FadeFromBlack();
    }
}
