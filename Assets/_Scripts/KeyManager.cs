using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    Dictionary<KeyCode, bool> balloonKeys;

    public const KeyCode upperLeft = KeyCode.W;
    public const KeyCode lowerLeft = KeyCode.D;
    public const KeyCode upperRight = KeyCode.O;
    public const KeyCode lowerRight = KeyCode.K;

    private Dictionary<KeyCode, GameObject> pregameKeys;

    [SerializeField]
    private BalloonPhysics bPhysics;

    // Start is called before the first frame update
    void Awake()
    {
        CreateDictionaries();
    }

    void CreateDictionaries()
    {
        balloonKeys = new Dictionary<KeyCode, bool>();
        balloonKeys.Add(upperLeft, false);
        balloonKeys.Add(lowerLeft, false);
        balloonKeys.Add(upperRight, false);
        balloonKeys.Add(lowerRight, false);

        this.pregameKeys = new Dictionary<KeyCode, GameObject>();
        this.pregameKeys.Add(upperLeft, GameObject.Find("wKey"));
        this.pregameKeys.Add(lowerLeft, GameObject.Find("dKey"));
        this.pregameKeys.Add(upperRight, GameObject.Find("oKey"));
        this.pregameKeys.Add(lowerRight, GameObject.Find("kKey"));
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateInput();
        if (GameManager.instance.preGameComplete == false)
        {
            this.HandlePregameStates();
            this.UpdatePregameState();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.preGameComplete == true)
        {
            this.ApplyBalloonPhysics();
        }
    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(upperLeft))
        {
            balloonKeys[upperLeft] = true;
        }
        if (Input.GetKeyDown(lowerLeft))
        {
            balloonKeys[lowerLeft] = true;
        }
        if (Input.GetKeyDown(upperRight))
        {
            balloonKeys[upperRight] = true;
        }
        if (Input.GetKeyDown(lowerRight))
        {
            balloonKeys[lowerRight] = true;
        }

        if (Input.GetKeyUp(upperLeft))
        {
            balloonKeys[upperLeft] = false;
        }
        if (Input.GetKeyUp(lowerLeft))
        {
            balloonKeys[lowerLeft] = false;
        }
        if (Input.GetKeyUp(upperRight))
        {
            balloonKeys[upperRight] = false;
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
                this.bPhysics.ToggleBalloonParticles(entry.Key, true);
            }
            else
            {
                this.bPhysics.ToggleBalloonParticles(entry.Key, false);
            }
        }

        this.bPhysics.ApplyBalloonDrag();
    }

    void HandlePregameStates()
    {
        foreach (KeyValuePair<KeyCode, bool> entry in this.balloonKeys)
        {
            if (entry.Value == false)
            {
                this.bPhysics.ToggleBalloonParticles(entry.Key, true);
                this.pregameKeys[entry.Key].SetActive(true);
            }
            else
            {
                this.bPhysics.ToggleBalloonParticles(entry.Key, false);
                this.pregameKeys[entry.Key].SetActive(false);
            }
        }
    }

    void UpdatePregameState()
    {
        bool allKeysHeld = true;

        foreach (KeyValuePair<KeyCode, bool> entry in this.balloonKeys)
        {
            if (entry.Value == false)
            {
                allKeysHeld = false;
            }
        }

        GameManager.instance.preGameComplete = allKeysHeld;

        if (GameManager.instance.preGameComplete == true)
        {
            PregameCamera.instance.ZoomOutCamera();
        }
    }
}
