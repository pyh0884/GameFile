using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTrap : MonoBehaviour
{
    [Range(0f,1f)]
    public float slowDownPercentage = 0.2f;

    private float playerSpeed = 0;
    private int playerLayer;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            other.gameObject.GetComponent<PlayerMovement>().MoveSpeed *= slowDownPercentage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {            
            other.gameObject.GetComponent<PlayerMovement>().MoveSpeed = other.gameObject.GetComponent<PlayerMovement>().initSpeed;
        }
    }
}
