using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PregameCamera : MonoBehaviour
{
    Transform balloonTransform;

    public static PregameCamera instance;

    private float zoomSpeed = 3f;

    private float targetZPosition = -7f;

    private Vector3 originalCameraPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.balloonTransform = GameObject.Find("Balloon").transform;
        this.originalCameraPosition = Camera.main.transform.position;

        if (PlayerPrefs.GetInt("tutorial", 0) == 0)
        {
            this.ZoomInCamera();
            PlayerPrefs.SetInt("tutorial", 1);
        }
    }

    public void ZoomInCamera()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomIn());
    }

    public void ZoomOutCamera()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomIn()
    {
        Vector3 targetPosition = new Vector3(this.balloonTransform.position.x, 
            this.balloonTransform.position.y, 
            this.targetZPosition);
        
        while (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.01f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, 
                targetPosition, this.zoomSpeed * Time.fixedDeltaTime);

            yield return null;
        }

        Camera.main.transform.position = targetPosition;
    }

    IEnumerator ZoomOut()
    {
        Vector3 targetPosition = this.originalCameraPosition;

        while (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.01f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                targetPosition, this.zoomSpeed * Time.fixedDeltaTime);

            yield return null;
        }

        Camera.main.transform.position = targetPosition;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("tutorial");
    }
}
