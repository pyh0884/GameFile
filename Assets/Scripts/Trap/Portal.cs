using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal targetPortal;
    public Transform targetPosition;
    private GameObject player;
    void Start()
    {
        //targetPosition = targetPortal.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && targetPortal)
        {
            Debug.Log(other.gameObject.name + "Teleport!");
            player = other.gameObject;
            StartCoroutine("Teleport");
        }
    }

    IEnumerator Teleport()
    {
        GetComponent<Collider>().enabled = false;
        player.GetComponent<PlayerMovement>().createDummy(targetPortal.targetPosition.position);
        player.SetActive(false);
        player.transform.position = targetPortal.targetPosition.position;
        yield return new WaitForSeconds(0.75f);
        player.SetActive(true);
        player = null;
        GetComponent<Collider>().enabled = true;
    }
}
