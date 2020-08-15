﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 movement;
    public float MoveSpeed;
    private float initSpeed;
    private bool isJumping;
    public Transform[] GroundCheck;
    public LayerMask GroundLayer;
    public GameObject dummy;
    private Camera camera;
    public float
        clampMarginMinX = 0.0f,
        clampMarginMaxX = 0.0f,
        clampMarginMinY = 0.0f,
        clampMarginMaxY = 0.0f;
    private float
        clampMinX,
        clampMaxX,
        clampMinY,
        clampMaxY;
    //冲刺相关
    private bool isDashing;
    public bool inShop;
    public float dashTime;
    private float dashTimer;
    public float dashSpeed;
    public float dashCD;
    private float dashCDTimer;
    public bool isPushing;

    //输入相关
    public int playerID = 10;
    private Player player;
    private void Awake()
    {
        FindObjectOfType<CinemachineTargetGroup>().AddMember(gameObject.transform, 1, 0);
    }
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        rb = GetComponent<Rigidbody>();
        initSpeed = MoveSpeed;
        camera = FindObjectOfType<Camera>();
    }
    public void getScreenData()
    {
        Ray MinX = camera.ScreenPointToRay(new Vector2(0 + clampMarginMinX, 0 + clampMarginMinY));
        Ray MaxX = camera.ScreenPointToRay(new Vector2(Screen.width - clampMarginMaxX, Screen.height - clampMarginMaxY));
        RaycastHit hit;
        if (Physics.Raycast(MinX, out hit,Mathf.Infinity,GroundLayer))
        {
            clampMinX = hit.point.x;
            clampMinY = hit.point.z;
        }
        if (Physics.Raycast(MaxX, out hit, Mathf.Infinity, GroundLayer))
        {
            clampMaxX = hit.point.x;
            clampMaxY = hit.point.z;
        }
    }
    private void Update()
    {
        if (isGround() && player.GetButtonDown("Jump"))
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
        //if (screenSize != camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight)))
        //{
        //    var bottomLeft = camera.ScreenToWorldPoint(Vector3.zero);
        //    screenSize = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight));
        //    cameraRect = new float[] { bottomLeft.x, bottomLeft.y, screenSize.x - bottomLeft.x, screenSize.y - bottomLeft.y };
        //    Debug.Log(cameraRect);
        //}
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
        if (rb.position.x < clampMinX && camera.fieldOfView == 30)
        {
            Debug.Log(clampMinX);
            movement.x = Mathf.Clamp(movement.x, 0, 1);
        }
        else if (rb.position.x > clampMaxX && camera.fieldOfView == 30)
        {
            Debug.Log(clampMaxX);
            movement.x = Mathf.Clamp(movement.x, -1, 0);
        }
        if (rb.position.z < clampMinY && camera.fieldOfView == 30)
        {
            Debug.Log(clampMinY);
            movement.z = Mathf.Clamp(movement.z, 0, 1);
        }
        else if (rb.position.z > clampMaxY && camera.fieldOfView == 30)
        {
            Debug.Log(clampMaxY);
            movement.z = Mathf.Clamp(movement.z, -1, 0);
        }
        ///修改坐标精确位移，可能会出现穿模
        rb.position = (rb.position + movement.normalized * MoveSpeed * Time.fixedDeltaTime);
        transform.LookAt(transform.position + movement);
        //rb.position = new Vector3(Mathf.Clamp(rb.position.x, clampMinX, clampMaxX), rb.position.y, Mathf.Clamp(rb.position.z, clampMinY, clampMaxY));
        
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
    private void LateUpdate()
    {
        getScreenData();
    }
    private void OnDestroy()
    {
        var dum = Instantiate(dummy, transform.position, Quaternion.identity);
        if (dum)
        {
            dum.GetComponent<DummyAI>().destination = new Vector3(125, 1.5f, 335);
            dum.transform.parent = null;
            var targetGroup = FindObjectOfType<CinemachineTargetGroup>();
            if (targetGroup)
            {
                targetGroup.AddMember(dum.transform, 1, 0);
                targetGroup.RemoveMember(gameObject.transform);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
            isPushing = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
            isPushing = false;
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
    /// 射线检测，判断是否在地面上
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
    }
}
