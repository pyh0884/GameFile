using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public Door door;
    private Rigidbody rigid;
    public GameObject mainObj;
    private Vector3 direction3;
    private bool isFalling;
    private float direction = 0;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isFalling)
        {
            direction -= Time.deltaTime * 20;
            //Debug.DrawRay(transform.position, new Vector3(10, direction, 0)*10,Color.blue);
            transform.LookAt(transform.position + new Vector3(0, direction, 0) + direction3);
        }
    }
    public void fall(Vector3 pos)
    {
        Vector3 newPos = new Vector3(pos.x, 0, pos.z);
        Vector3 selfPos = new Vector3(transform.position.x, 0, transform.position.z);
        direction3 = (newPos - selfPos).normalized;
        isFalling = true;
        if (door) Destroy(door);

        rigid.isKinematic = false;
        rigid.velocity = Vector3.zero;
        GetComponent<Collider>().isTrigger = true;
        rigid.useGravity = true;
        Destroy(mainObj, 3);
    }
}
