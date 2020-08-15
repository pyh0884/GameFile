using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinManager CoinManager;
    public int CoinValue;
    void Start()
    {
        CoinManager = FindObjectOfType<CoinManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
            CoinManager.AddCoin(CoinValue);
        Destroy(gameObject);
    }
}
