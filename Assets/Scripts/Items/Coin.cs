using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GamingScene gamingScene;
    public int CoinValue;
    private AudioSource audio;
    public AudioClip coinCollect;

    void Start()
    {
        gamingScene = FindObjectOfType<GamingScene>();
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            gamingScene.AddCoin(CoinValue, other.GetComponent<PlayerMovement>().playerID);
            audio.PlayOneShot(coinCollect);
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(gameObject, 0.5f);
        }
    }
}
