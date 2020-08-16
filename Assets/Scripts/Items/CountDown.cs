using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Slider slider;
    public float countDown=3;
    private float countDownTimer = 3;
    private Camera camera;
 
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        countDownTimer = countDown;
        slider.value = 1;
        Destroy(gameObject, (countDown + 2));
    }

    void FixedUpdate()
    {
        countDownTimer -= Time.fixedDeltaTime;
        slider.value = countDownTimer / countDown;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
    }
}
