using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class BalloonPhysics : MonoBehaviour
{
    [SerializeField]
    private Rigidbody balloonRb;
    [SerializeField]
    private Transform balloonTransform;

    private Vector3 downRight = new Vector3(1.0f, -1.0f, 0.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 upRight = new Vector3(1.0f, 1.0f, 0.0f);
    private Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 downLeft = new Vector3(-1.0f, -1.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 upLeft = new Vector3(-1.0f, 1.0f, 0.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private float forceMagnitude = 7f;
    private float dragMagnitude = 1f;
    private float liftMagnitude = 4f;
    private float knotGravityMagnitude = 2f;
    private float bounceMagnitude = 5f;
    private float deflateMagnitude = 30f;

    private Vector3 maxScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 minScale = new Vector3(0.3f, 0.3f, 0.3f);

    private float maxMass = 1f;
    private float minMass = 0.5f;

    [SerializeField]
    private List<ParticleSystem> propulsionParticles;

    [SerializeField]
    private ParticleSystem deathParticles;

    private float airLevel = 100f;
    private float maxAirLevel = 100f;
    private float drainRate = 0.1f;
    private float deflateRate = 0.5f;

    [SerializeField]
    private Text tempAirLevelUI;

    private bool dead = false;

    public delegate void OnLevelFinished();
    public static OnLevelFinished LevelFinished;

    // Start is called before the first frame update
    void Start()
    {
        this.balloonRb.centerOfMass = new Vector3(0.0f, -0.3f, 0.0f);
    }

    private void Update()
    {
        this.tempAirLevelUI.text = this.airLevel.ToString();

        this.balloonTransform.localScale = Vector3.Lerp(this.minScale, this.maxScale, this.airLevel / this.maxAirLevel);
        this.balloonRb.mass = Mathf.Lerp(this.minMass, this.maxMass, this.airLevel / this.maxAirLevel);

        if (this.airLevel < 0 && this.dead == false)
        {
            this.balloonRb.useGravity = true;
            this.balloonRb.mass = 20f;
            this.dead = true;

            //this.balloonRb.AddForce( * 10.0f, ForceMode.Impulse);

            StartCoroutine(ResetLevel());
        }
    }

    public void ApplyBalloonPhysics(KeyCode direction)
    {
        if (this.airLevel < 0 || this.dead == true)
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
        }
    }

    public void ApplyBalloonDrag()
    {
        if (this.dead == false)
        {
            this.balloonRb.AddForceAtPosition(Vector3.up * this.liftMagnitude,
                this.balloonTransform.position + (Vector3.up * 2.0f));
        }

        this.balloonRb.AddForceAtPosition(Vector3.down * this.knotGravityMagnitude, 
            this.balloonTransform.position + (Vector3.down * 2.0f));

        Vector3 dragDirection = -this.balloonRb.velocity;
        this.balloonRb.AddForce(dragDirection * dragMagnitude);
    }

    public void ToggleBalloonParticles(KeyCode direction, bool active)
    {
        if (this.airLevel < 0 || this.dead == true)
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

    private void KillBalloon(Vector3 collisionPoint)
    {
        StartCoroutine(this.DefalteBalloon(collisionPoint));
    }

    private IEnumerator DefalteBalloon(Vector3 collisionPoint)
    {
        this.balloonRb.useGravity = true;

        Vector3 killDirection = collisionPoint - this.balloonTransform.position;
        float killRotation = Vector3.Angle(killDirection, killDirection + Vector3.right);

        ShapeModule editableShape = this.deathParticles.shape;
        editableShape.rotation = new Vector3(killRotation, (killDirection.x > 0 ? 90.0f : -90.0f), 0.0f);
        this.deathParticles.Play();

        this.balloonRb.AddForce(-killDirection.normalized * 10.0f, ForceMode.Impulse);
        while (this.airLevel > 0)
        {
            this.airLevel -= this.deflateRate;

            this.balloonRb.AddForceAtPosition(-killDirection.normalized * this.deflateMagnitude,
            this.balloonTransform.position + (Vector3.up * 5.0f));

            yield return null;
        }
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hazard")
        {
            this.KillBalloon(collision.GetContact(0).point);
        }     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "finish")
        {
            if (LevelFinished != null)
            {
                LevelFinished();
            }
        }
    }
}
