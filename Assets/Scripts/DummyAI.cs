using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAI : MonoBehaviour
{
    public float lerpTime;
    public Vector3 destination;
    [Header("玩家复活时间")]
    public float ReviveTime = 3;
    private void Start()
    {
        Destroy(gameObject, ReviveTime + 1);
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destination, lerpTime);

    }
}