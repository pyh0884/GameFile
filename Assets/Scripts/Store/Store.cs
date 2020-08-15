using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    private int playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            other.gameObject.GetComponent<PlayerMovement>().inShop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            other.gameObject.GetComponent<PlayerMovement>().inShop = false;
        }
    }
}
