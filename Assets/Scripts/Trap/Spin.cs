using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private float rotate;
    public float rotateSpeed = 50;

    void Update()
    {
        rotate += rotateSpeed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, rotate, transform.eulerAngles.z);
    }
}
