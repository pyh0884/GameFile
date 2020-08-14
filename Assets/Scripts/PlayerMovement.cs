using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    public int playerID = 10;
    private Player player;
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.GetButtonDown("Item2"))
        //{
        //    UseItem(items[1]);
        //    items[1] = 0;
        //    FindObjectOfType<TopCanvas>().ChangeItem(playerID, items);
        //}
        //movement.x = player.GetAxisRaw("MoveHorizontal");
        //movement.y = player.GetAxisRaw("MoveVertical");
    }
}
