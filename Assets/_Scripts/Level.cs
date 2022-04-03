using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    public string sceneName;
    public string levelName;
    public string difficulty;
    public int playerScore;
    public string privateLeaderboardKey;
    public string publicLeaderboardKey;
    public string imageFileName;
}
