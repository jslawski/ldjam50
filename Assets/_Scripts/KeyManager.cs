using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    Dictionary<KeyCode, bool> balloonKeys;

    public const KeyCode upperLeft = KeyCode.W;
    public const KeyCode left = KeyCode.Alpha9;
    public const KeyCode lowerLeft = KeyCode.D;
    public const KeyCode upperRight = KeyCode.O;
    public const KeyCode right = KeyCode.Alpha0;
    public const KeyCode lowerRight = KeyCode.K;
    public const KeyCode up = KeyCode.Alpha1;
    public const KeyCode down = KeyCode.Alpha2;

    [SerializeField]
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
        balloonKeys.Add(up, false);
        balloonKeys.Add(down, false);
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateInput();
    }

    private void FixedUpdate()
    {
        this.ApplyBalloonPhysics();
    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

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
        if (Input.GetKeyDown(up))
        {
            balloonKeys[up] = true;
        }
        if (Input.GetKeyDown(down))
        {
            balloonKeys[down] = true;
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
        if (Input.GetKeyUp(up))
        {
            balloonKeys[up] = false;
        }
        if (Input.GetKeyUp(down))
        {
            balloonKeys[down] = false;
        }
    }

    void ApplyBalloonPhysics()
    {
        foreach (KeyValuePair<KeyCode, bool> entry in this.balloonKeys)
        {
            if (entry.Value == false)
            {
                this.bPhysics.ApplyBalloonPhysics(entry.Key);
                this.bPhysics.ToggleBalloonParticles(entry.Key, true);
            }
            else
            {
                this.bPhysics.ToggleBalloonParticles(entry.Key, false);
            }
        }

        this.bPhysics.ApplyBalloonDrag();
    }
}
