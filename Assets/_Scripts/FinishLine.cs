using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private BoxCollider finishCollider;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            this.finishCollider.enabled = false;
        }
    }
}
