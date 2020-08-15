using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public GameObject[] doorsConnected;

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
        if(other.gameObject.layer == playerLayer)
        {
            foreach(var i in doorsConnected)
            {
                i.GetComponent<Door>().DoorOpenAction();
            }
        }
    }
}
