using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private BoxCollider finishCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        
        BalloonPhysics.LevelFinished += this.FinishLevel;
    }

    private void FinishLevel(int score)
    {
        Debug.LogError("Yey you win!");

        this.finishCollider.enabled = false;

        HighScores.UploadScore(PlayerPrefs.GetString("name", ""), score);

        BalloonPhysics.LevelFinished -= this.FinishLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
