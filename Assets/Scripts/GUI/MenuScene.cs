using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class MenuScene : MonoBehaviour
{
    public Animator animator;
    private float clipTime;
    private static List<AnimatorClipInfo> clipList = new List<AnimatorClipInfo>();
    public void Click()
    {
        Debug.Log("Button Clicked");
        playAnimation(animator);
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
