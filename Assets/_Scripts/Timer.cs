using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<int> timerValues = GameManager.instance.GetTimerValues();

        string minutesValue = (timerValues[0] > 9) ? timerValues[0].ToString() : "0" + timerValues[0].ToString();
        string secondsValue = (timerValues[1] > 9) ? timerValues[1].ToString() : "0" + timerValues[1].ToString();
        string millisecondsValue = (timerValues[2] > 9) ? timerValues[2].ToString() : "0" + timerValues[2].ToString();


        this.timerText.text = minutesValue + ":" + secondsValue + ":" + millisecondsValue;
    }
}
