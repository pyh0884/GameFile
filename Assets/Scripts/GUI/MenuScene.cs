using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class MenuScene : MonoBehaviour
{
    public GameObject ui;
    public Animator animator;
    public GameObject dialog;
    public GameObject dialog01;
    public GameObject dialog02;
    private static List<AnimatorClipInfo> clipList = new List<AnimatorClipInfo>();
    void Start()
    {
        ui = GameObject.Find("UI");
        animator = ui.GetComponent<Animator>();
        dialog.SetActive(false);
        dialog01.SetActive(false);
        dialog02.SetActive(false);
    }
    public void Click()
    {
        Debug.Log("Button Clicked");
        playAnimation(animator);
    }
    public void Popup()
    {
        Debug.Log("Setting Clicked");
        dialog.SetActive(true);
    }
    public void Next()
    {
        Debug.Log("OK Clicked");
        dialog.SetActive(false);
        dialog01.SetActive(true);
    }
    public void NextAgain()
    {
        Debug.Log("OK Clicked");
        dialog01.SetActive(false);
        dialog02.SetActive(true);
    }
    public void End()
    {
        Debug.Log("End Clicked");
        dialog02.SetActive(false);
    }
    public void playAnimation(Animator animator)
    {
        StartCoroutine(playAnim(animator));
    }
    public static IEnumerator playAnim(Animator animator)
    {
        animator.SetBool("isShow", false);
        animator.GetCurrentAnimatorClipInfo(0, clipList);
        AnimationClip m_clip = clipList[0].clip;
        yield return new WaitForSeconds(m_clip.length);
        SceneManager.LoadScene(1);
    }
}
