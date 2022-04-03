using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField]
    private GameObject playerName;
    [SerializeField]
    private TextMeshProUGUI playerScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerScore.text == "9999")
        {
            this.playerName.SetActive(false);
            this.playerScore.gameObject.SetActive(false);
        }
        else
        {
            this.playerName.SetActive(true);
            this.playerScore.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        this.playerScore.text = "9999";
    }
}
