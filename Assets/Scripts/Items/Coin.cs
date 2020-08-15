using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GamingScene gamingScene;
    public int CoinValue;
    void Start()
    {
        gamingScene = FindObjectOfType<GamingScene>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
            gamingScene.AddCoin(CoinValue, other.GetComponent<PlayerMovement>().playerID);
        Destroy(gameObject);
    }
}
