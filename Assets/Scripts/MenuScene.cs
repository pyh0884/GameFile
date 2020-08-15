using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class MenuScene : MonoBehaviour
{
    public GameObject ui;
    private Animator animator;
    private bool ifend = false;
  

    void Start()
    {
        ui = GameObject.Find("UI");
        animator = ui.GetComponent<Animator>();
    }
    public void loadgame()
    {
        SceneManager.LoadScene("Saddog_Gaming_Scene");
    }

    void PlayShowAnimation()
    {
        animator.SetBool("isShow", false);
    }
    public void Click()
    {
        Debug.Log("Button Clicked");
        GameManager.instance.PlayAnimation(animator, PlayShowAnimation, loadgame);
        //animator.SetBool("isShow", false);
        //ifend = true;
        //animator.GetCurrentAnimatorClipInfo(0, clipList);
        //AnimationClip m_clip = clipList[0].clip;
        //clipTime = m_clip.length;     
    }



    void Update()
    {

    }


}
