using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public int playerID = 10;
    private Player player;
    private Rigidbody rb;
    private Vector3 movement;
    public float MoveSpeed;
    public bool isJumping;
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (player.GetButtonDown("Jump"))
        {
            //rb.velocity = new Vector3(rb.velocity.x, 50, rb.velocity.z);
            rb.velocity += Vector3.up * 10;
            //rb.AddForce(new Vector3(0, 50, 0), ForceMode.Impulse);
            isJumping = true;
        }
    }
    public bool isGround() 
    {
        bool isHit = false;

        return isHit;
    }
    /// <summary>
    /// 所有关于物理的运算全部放在FixedUpdate中
    /// </summary>
    void FixedUpdate()
    {
        movement.x = player.GetAxisRaw("MoveHorizontal");
        movement.z = player.GetAxisRaw("MoveVertical");
        ///TODO: 射线检测地面
        if (rb.velocity.y == 0)
            isJumping = false;
        ///修改速度精确位移
        if (isJumping && rb.velocity.y < 0)///下降
        {
            rb.velocity = new Vector3(0, Mathf.Clamp(rb.velocity.y * 1.2f, -25, 0), 0);
        }
        else if (isJumping && rb.velocity.y < 5 && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        ///修改坐标精确位移，可能会出现穿模
        rb.position = (rb.position - movement * MoveSpeed * Time.fixedDeltaTime);
        //rb.AddForce(-movement * MoveSpeed * Time.fixedDeltaTime); ---带有惯性的移动
    }
}
