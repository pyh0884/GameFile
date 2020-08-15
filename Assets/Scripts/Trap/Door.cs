using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   
    public bool isOpen = false;
    

    private Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void DoorOpenAction()
    {
        isOpen = true;
        doorAnimator.SetBool("isOpen", true);
    }
}
