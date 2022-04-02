using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonPhysics : MonoBehaviour
{
    [SerializeField]
    private Rigidbody balloonRb;

    private Vector3 downRight = new Vector3(1.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 upRight = new Vector3(1.0f, 1.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 downLeft = new Vector3(-1.0f, -1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 upLeft = new Vector3(-1.0f, 1.0f, 0.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private float forceMagnitude = 5f;
    private float dragMagnitude = 1f;

    [SerializeField]
    private List<ParticleSystem> propulsionParticles;

    private float airLevel = 100f;
    private float drainRate = 0.1f;

    [SerializeField]
    private Text tempAirLevelUI;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    private void Update()
    {
        this.tempAirLevelUI.text = this.airLevel.ToString();

        if (this.airLevel < 0)
        {
            this.balloonRb.useGravity = true;
        }
    }

    public void ApplyBalloonPhysics(KeyCode direction)
    {
        if (this.airLevel < 0)
        {
            return;
        }

        switch (direction)
        {
            case KeyManager.upperLeft:
                this.balloonRb.AddForce(this.downRight.normalized * forceMagnitude);
                this.airLevel -= this.drainRate;
                break;
            case KeyManager.lowerLeft:
                this.balloonRb.AddForce(this.upRight.normalized * forceMagnitude);
                this.airLevel -= this.drainRate;
                break;
            case KeyManager.upperRight:
                this.balloonRb.AddForce(this.downLeft.normalized * forceMagnitude);
                this.airLevel -= this.drainRate;
                break;
            case KeyManager.lowerRight:
                this.balloonRb.AddForce(this.upLeft.normalized * forceMagnitude);
                this.airLevel -= this.drainRate;
                break;
                //case KeyManager.left:
                //  this.balloonRb.AddForce(this.right.normalized * forceMagnitude);
                //  break;

                //case KeyManager.right:
                //   this.balloonRb.AddForce(this.left.normalized * forceMagnitude);
                //   break;

                //case KeyManager.up:
                //    this.balloonRb.AddForce(this.down.normalized * forceMagnitude);
                //    break;
                //case KeyManager.down:
                //    this.balloonRb.AddForce(this.up.normalized * forceMagnitude);
                //    break;
        }
    }

    public void ApplyBalloonDrag()
    {
        Vector3 dragDirection = -this.balloonRb.velocity;
        this.balloonRb.AddForce(dragDirection * dragMagnitude);
    }

    public void ToggleBalloonParticles(KeyCode direction, bool active)
    {
        if (this.airLevel < 0)
        {
            for (int i = 0; i < this.propulsionParticles.Count; i++)
            {
                this.propulsionParticles[i].Stop();
            }
        }

        switch (direction)
        {
            case KeyManager.upperLeft:
                if (active)
                {
                    this.propulsionParticles[0].Play();
                }
                else
                {
                    this.propulsionParticles[0].Stop();
                }
                break;
            case KeyManager.left:
                if (active)
                {
                    this.propulsionParticles[1].Play();
                }
                else
                {
                    this.propulsionParticles[1].Stop();
                }
                break;
            case KeyManager.lowerLeft:
                if (active)
                {
                    this.propulsionParticles[2].Play();
                }
                else
                {
                    this.propulsionParticles[2].Stop();
                }
                break;
            case KeyManager.upperRight:
                if (active)
                {
                    this.propulsionParticles[3].Play();
                }
                else
                {
                    this.propulsionParticles[3].Stop();
                }
                break;
            case KeyManager.right:
                if (active)
                {
                    this.propulsionParticles[4].Play();
                }
                else
                {
                    this.propulsionParticles[4].Stop();
                }
                break;
            case KeyManager.lowerRight:
                if (active)
                {
                    this.propulsionParticles[5].Play();
                }
                else
                {
                    this.propulsionParticles[5].Stop();
                }
                break;
            case KeyManager.up:
                if (active)
                {
                    this.propulsionParticles[6].Play();
                }
                else
                {
                    this.propulsionParticles[6].Stop();
                }
                break;
            case KeyManager.down:
                if (active)
                {
                    this.propulsionParticles[7].Play();
                }
                else
                {
                    this.propulsionParticles[7].Stop();
                }
                break;
        }
    }
}
