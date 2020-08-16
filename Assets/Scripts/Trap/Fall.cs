using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public Door door;
    public Rigidbody[] rigids;
    public GameObject mainObj;
    public Collider[] cols;
    public void fall()
    {
        if (door) Destroy(door);
        foreach (Rigidbody rb in rigids)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            if (cols.Length == 0)
                rb.gameObject.GetComponent<Collider>().isTrigger = true;
            else
            {
                foreach (Collider col in cols)
                {
                    col.isTrigger = true;
                }
            }
            rb.useGravity = true;
        }
        Destroy(mainObj, 3);
    }
}
