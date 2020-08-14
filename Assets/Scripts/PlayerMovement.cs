using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]public int playerID = 10;
    private Player player;
    private Rigidbody rb;
    private Vector3 movement;
    public float MoveSpeed;
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (player.GetButtonDown("Item2"))
        //{
        //    UseItem(items[1]);
        //    items[1] = 0;
        //    FindObjectOfType<TopCanvas>().ChangeItem(playerID, items);
        //}
        movement.x = player.GetAxisRaw("MoveHorizontal");
        movement.z = player.GetAxisRaw("MoveVertical");
        rb.position = (rb.position - movement * MoveSpeed * Time.fixedDeltaTime);
    }
}
