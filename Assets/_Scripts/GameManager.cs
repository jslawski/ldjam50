using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BalloonPhysics.LevelFinished += this.FinishLevel;  
    }

    private void FinishLevel()
    {
        Debug.LogError("Yey you win!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
