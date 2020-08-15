using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject bloodBar;
    public GameObject coinsText;
    public GameObject boxText;


  
    public void SetCoinsAmount(int amount)
    {
        //每次调用dowteen都会返回一个 tweener
        var tween =  boxText.transform.DOMove(new Vector3(1,1,0),2f);
        //tweener里可以add回调到complete的委托
        tween.onComplete += () => { Debug.Log(""); };
        tween.onComplete += Callback;

       
    }

    void Callback()
    {
        
    }

    public void SetBoxAmount(int amount)
    {
        transform.Find("Nbox").GetComponent<Text>().text = amount.ToString();
    }

    public void SetBlood()
    {

    }
}
