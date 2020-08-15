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
    private float initSpeed;
    private bool isJumping;
    private bool isDashing;
    public float dashTime;
    private float dashTimer;
    public float dashSpeed;
    public float dashCD;
    private float dashCDTimer;
    public Transform[] GroundCheck;
    public LayerMask GroundLayer;
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        rb = GetComponent<Rigidbody>();
        initSpeed = MoveSpeed;
    }
    private void Update()
    {
        if (isGround()&&player.GetButtonDown("Jump"))
        {
            //rb.velocity = new Vector3(rb.velocity.x, 50, rb.velocity.z);
            rb.velocity += Vector3.up * 10;
            //rb.AddForce(new Vector3(0, 50, 0), ForceMode.Impulse);
            isJumping = true;
        }
        if (player.GetButtonDown("Dash") && dashCDTimer <= 0)
        {
            dashTimer = dashTime;
            Dash();
            dashCDTimer = dashCD;
        }
        if (dashCDTimer > 0) dashCDTimer -= Time.deltaTime;
        if (isDashing) dashTimer -= Time.deltaTime;
    }
    // 所有关于物理的运算全部放在FixedUpdate中
    void FixedUpdate()
    {
        movement.x = player.GetAxisRaw("MoveHorizontal");
        movement.z = player.GetAxisRaw("MoveVertical");
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
        rb.position = (rb.position + movement * MoveSpeed * Time.fixedDeltaTime);
        //rb.AddForce(movement * MoveSpeed * Time.fixedDeltaTime); ---带有惯性的移动
        //冲刺
        if (isDashing)
        {
            MoveSpeed = dashSpeed;
            if (dashTimer <= 0)
            {
                MoveSpeed = initSpeed;
                isDashing = false;
                //rb.useGravity = true;
            }
        }
    }
    void Dash() 
    {
        //地面冲刺
        if (isGround())
        {
            isDashing = true;
        }
        //空中冲刺
        else
        {
            isDashing = true;
            //rb.useGravity = false;
        }
    }
    #region Data Part
    /// <summary>
    /// 射线检测，判断是否在地面上
    /// </summary>
    public bool isGround()
    {
        bool isHit = false;
        foreach (Transform trans in GroundCheck)
        {
            Debug.DrawLine(trans.position, new Vector3(trans.position.x, trans.position.y - 0.15f, trans.position.z), Color.blue);
            if (Physics.Linecast(trans.position, new Vector3(trans.position.x, trans.position.y - 0.15f, trans.position.z), GroundLayer))
            {
                isHit = true;
            }
        }
        return isHit;
    #endregion

    }
}
