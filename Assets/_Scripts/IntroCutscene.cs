using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer cutscenePlayer;

    private bool hasStarted = false;

    private bool isTransitioning = false;

    private void Awake()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "introCutscene.mp4");
        this.cutscenePlayer.url = filePath;

        this.cutscenePlayer.renderMode = VideoRenderMode.RenderTexture;
        this.cutscenePlayer.targetCameraAlpha = 1.0f;
        this.cutscenePlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.cutscenePlayer.isPlaying && this.hasStarted == false)
        {
            this.hasStarted = true;
        }

        if (!this.cutscenePlayer.isPlaying && this.hasStarted == true)
        {
            this.gameObject.SetActive(false);

            if (this.isTransitioning == false)
            {
                this.isTransitioning = true;
                this.LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        string playerName = PlayerPrefs.GetString("name", "");

        if (playerName == "")
        {
            SceneLoader.instance.LoadScene("LoginScene");
        }
        else
        {
            SceneLoader.instance.LoadScene("MainMenu");
        }

    }
}
