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
    public Text foodText1;
    public Text foodText2;
    public Transform health1;
    public Transform health2;

    public int Coins1;
    public int Coins2;
    public int Food1;
    public int Food2;
    private int coinsCollected = 0;
    public int TotalCoins = 15;
    void Start()
    {
        Coins1 = 0;
        Coins2 = 0;
        Food1 = 0;
        Food2 = 0;
        RefreshCoins();
        RefreshFood();
    }

    void Update()
    {
        if (coinsCollected >= TotalCoins)
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
        if (num > 0) coinsCollected += num;
        RefreshCoins();
    }
    void RefreshCoins()
    {
        coinsText1.text = "X " + Coins1;
        coinsText2.text = "X " + Coins2;
    }
    void RefreshFood()
    {
        if (Food1 > 0)
        {
            foodText1.text = "Have Food!";
        }
        else 
        {
            foodText1.text = "No Food!";
        }
        if (Food2 > 0)
        {
            foodText2.text = "Have Food!";
        }
        else
        {
            foodText2.text = "No Food!";
        }
    }
    public void checkCoins(int id, int num)
    {
        if (id == 0 && Food1 <= 0 && Coins1 >= num)
        {
            AddCoin(-num, id);
            addFood(id, 1);
        }
        else if (id == 1 && Food2 <= 0 && Coins2 >= num)
        {
            AddCoin(-num, id);
            addFood(id, 1);
        }
    }
    public void addFood(int id, int num) 
    {
        if (id == 0)
            Food1 += num;
        else if (id == 1)
            Food2 += num;
        RefreshFood();
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
