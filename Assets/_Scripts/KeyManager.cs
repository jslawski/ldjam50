using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    Dictionary<KeyCode, bool> balloonKeys;

    static KeyCode upperLeft = KeyCode.E;
    static KeyCode left = KeyCode.D;
    static KeyCode lowerLeft = KeyCode.C;
    static KeyCode upperRight = KeyCode.O;
    static KeyCode right = KeyCode.K;
    static KeyCode lowerRight = KeyCode.M;

    private BalloonPhysics bPhysics;

    // Start is called before the first frame update
    void Awake()
    {
        CreateDictionary();
    }

    void CreateDictionary()
    {
        balloonKeys = new Dictionary<KeyCode, bool>();
        balloonKeys.Add(upperLeft, false);
        balloonKeys.Add(left, false);
        balloonKeys.Add(lowerLeft, false);
        balloonKeys.Add(upperRight, false);
        balloonKeys.Add(right, false);
        balloonKeys.Add(lowerRight, false);
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateInput();
        this.ApplyBalloonPhysics();
    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(upperLeft))
        {
            balloonKeys[upperLeft] = true;
        }
        if (Input.GetKeyDown(left))
        {
            balloonKeys[left] = true;
        }
        if (Input.GetKeyDown(lowerLeft))
        {
            balloonKeys[lowerLeft] = true;
        }
        if (Input.GetKeyDown(upperRight))
        {
            balloonKeys[upperRight] = true;
        }
        if (Input.GetKeyDown(right))
        {
            balloonKeys[right] = true;
        }
        if (Input.GetKeyDown(lowerRight))
        {
            balloonKeys[lowerRight] = true;
        }

        if (Input.GetKeyUp(upperLeft))
        {
            balloonKeys[upperLeft] = false;
        }
        if (Input.GetKeyUp(left))
        {
            balloonKeys[left] = false;
        }
        if (Input.GetKeyUp(lowerLeft))
        {
            balloonKeys[lowerLeft] = false;
        }
        if (Input.GetKeyUp(upperRight))
        {
            balloonKeys[upperRight] = false;
        }
        if (Input.GetKeyUp(right))
        {
            balloonKeys[right] = false;
        }
        if (Input.GetKeyUp(lowerRight))
        {
            balloonKeys[lowerRight] = false;
        }
    }

    void ApplyBalloonPhysics()
    {
        foreach (KeyValuePair<KeyCode, bool> entry in this.balloonKeys)
        {
            if (entry.Value == false)
            {
                this.bPhysics.ApplyBalloonPhysics(entry.Key);
            }
        }
    }
}
