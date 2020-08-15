using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }


    #region 动画控制
    private float clipTime;
    private static List<AnimatorClipInfo> clipList = new List<AnimatorClipInfo>();
    public void PlayAnimation(Animator animator, Action SetParams, Action Callback)
    {
        StartCoroutine(PlayAniamtion(animator, SetParams, Callback));
    }

    public static IEnumerator PlayAniamtion(Animator animator, Action SetParams, Action Callback)
    {
        SetParams?.Invoke();
        animator.GetCurrentAnimatorClipInfo(0, clipList);
        AnimationClip m_clip = clipList[0].clip;
        yield return new WaitForSeconds(m_clip.length);
        Callback?.Invoke();
    }

    #endregion
}
