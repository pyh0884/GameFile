using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public Door door;
    public Rigidbody[] rigids;
    public GameObject mainObj;
    public void fall()
    {
        if (door) Destroy(door);
        foreach (Rigidbody rb in rigids)
        {
            rb.gameObject.GetComponent<Collider>().isTrigger = true;
            rb.useGravity = true;
        }
        Destroy(mainObj, 3);
    }
}
