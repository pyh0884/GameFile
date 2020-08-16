using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public float idleToRunSpeed = 0.2f;
    private Rigidbody rg;
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vol = rg.velocity;
        float speed = Mathf.Abs(vol.x) + Mathf.Abs(vol.y) + Mathf.Abs(vol.z);
        if (speed > idleToRunSpeed)
        {
            ani.SetBool("running", true);
        }
        else
        {
            ani.SetBool("running", false);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
