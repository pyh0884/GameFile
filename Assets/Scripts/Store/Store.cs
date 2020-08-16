using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public int foodCost = 4;
    private AudioSource asrc;
    public AudioClip buyFood;
    // Start is called before the first frame update
    void Start()
    {
        asrc = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<PlayerMovement>().inShop = true;
            FindObjectOfType<GamingScene>().checkCoins(other.GetComponent<PlayerMovement>().playerID, foodCost);
            asrc.PlayOneShot(buyFood);
            Debug.Log(other.name + " entered the store.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<PlayerMovement>().inShop = false;
            Debug.Log(other.name + " exited the store.");
        }
    }
}
