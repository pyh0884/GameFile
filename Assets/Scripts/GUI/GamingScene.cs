using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using TreeEditor;

public class GamingScene : MonoBehaviour
{
    public Text coinsText1;
    public Text coinsText2;
    public Transform health1;
    public Transform health2;

    public int Coins1;
    public int Coins2;
    public int TotalCoins = 15;
    void Start()
    {
        Coins1 = 0;
        Coins2 = 0;
        RefreshCoins();
    }

    void Update()
    {
        if (Coins1 + Coins2 >= TotalCoins)
        {
            FindObjectOfType<GameManager>().Win();
        }
        TestMinusBlood();
        RefreshHealth();
    }
    public void AddCoin(int num, int playerID)
    {
        if (playerID == 0)
            Coins1 += num;
        else
            Coins2 += num;
        RefreshCoins();
    }
    void RefreshCoins()
    {
        coinsText1.text = "X " + Coins1;
        coinsText2.text = "X " + Coins2;
    }

    void RefreshHealth()
    {
        for(int i = 0;i< health1.childCount;i++)
        {
            health1.GetChild(i).gameObject.SetActive(false);
            health2.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 1;i<=GameManager.instance.P1Lifes;i++)
        {
            health1.GetChild(i-1).gameObject.SetActive(true);
        }
        for (int i = 1; i <= GameManager.instance.P2Lifes; i++)
        {
            health2.GetChild(i - 1).gameObject.SetActive(true);
        }
    }

    void TestMinusBlood()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.instance.P2Lifes--;
         
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.P1Lifes--;
            
        }
    }
}
