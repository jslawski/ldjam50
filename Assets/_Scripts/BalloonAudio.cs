using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonAudio : MonoBehaviour
{
    public static BalloonAudio instance;

    [SerializeField]
    private AudioSource balloonAir;
    [SerializeField]
    private AudioSource balloonDeflate;
    [SerializeField]
    private AudioSource balloonPop;
    [SerializeField]
    private AudioSource balloonFail;
    [SerializeField]
    private AudioSource balloonHit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private float GetRandomPitch()
    {
        return Random.Range(0.7f, 1.3f);
    }

    public void PlayBalloonAir()
    {
        if (this.balloonAir.isPlaying == false)
        {
            this.balloonAir.pitch = this.GetRandomPitch();
            this.balloonAir.Play();
        }
    }

    public void StopBalloonAir()
    {
        this.balloonAir.Stop();
    }

    public void PlayBalloonDeflate()
    {
        if (this.balloonDeflate.isPlaying == false)
        {
            this.balloonDeflate.pitch = this.GetRandomPitch();
            this.balloonDeflate.Play();
        }
    }

    public void PlayBalloonPop()
    {
        if (this.balloonPop.isPlaying == false)
        {
            this.balloonPop.pitch = this.GetRandomPitch();
            this.balloonPop.Play();
        }
    }

    public void PlayBalloonFail()
    {
        if (this.balloonFail.isPlaying == false)
        {
            this.balloonFail.Play();
        }
    }

    public void PlayBalloonHit()
    {
        if (this.balloonHit.isPlaying == false)
        {
            this.balloonHit.pitch = this.GetRandomPitch();
            this.balloonHit.Play();
        }
    }
}
