using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public LayerMask FoodLayer;
    public LayerMask PlayerLayer;
    public Vector3 WarningArea;
    public float CheckTargetTime = 2;
    private float CheckTimer = 2;
    private Collider nearestTarget = null;
    private bool isConsuming;
    public bool rotating;
    public float deltaTime = 0.5f;
    private Rigidbody rb;
    public float MoveSpeed;
    public float ConsumeTime = 3;
    public AudioClip swallow;
    private AudioSource asrc;
    public GameObject countDownCanvas;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        asrc = GetComponent<AudioSource>();
    }

    private Collider FindTarget()
    {
        Collider temp = nearestTarget;
        Collider[] foodList = Physics.OverlapBox(gameObject.transform.position, WarningArea, Quaternion.identity, FoodLayer);
        Collider[] playerList = Physics.OverlapBox(gameObject.transform.position, WarningArea, Quaternion.identity, PlayerLayer);
        if (foodList.Length > 0)
        {
            nearestTarget = foodList[0];
            foreach (Collider col in foodList)
            {
                if (!nearestTarget)
                {
                    nearestTarget = col;
                }
                else if ((col.transform.position - transform.position).magnitude < (nearestTarget.transform.position - transform.position).magnitude)
                {
                    nearestTarget = col;
                }
            }
        }
        else if (playerList.Length > 0)
        {
            nearestTarget = playerList[0];
            foreach (Collider col in playerList)
            {
                if (col.GetComponent<PlayerMovement>())
                {
                    if (!nearestTarget && !col.GetComponent<PlayerMovement>().inShop)
                    {
                        nearestTarget = col;
                    }
                    else if ((col.transform.position - transform.position).magnitude < (nearestTarget.transform.position - transform.position).magnitude && !col.GetComponent<PlayerMovement>().inShop)
                    {
                        nearestTarget = col;
                    }
                }
            }
        }
        if (nearestTarget != temp)
            rotating = true;
        return nearestTarget;
    }
    public IEnumerator ConsumeFood()
    {
        nearestTarget = null;
        transform.localScale *= 2;
        isConsuming = true;
        GetComponent<Collider>().enabled = false;
        asrc.PlayOneShot(swallow);
        yield return new WaitForSeconds(ConsumeTime);
        isConsuming = false;
        FindTarget();
        GetComponent<Collider>().enabled = true;
    }
    public IEnumerator ConsumePeople(float deltaTime)
    {
        nearestTarget = null;
        isConsuming = true;
        GetComponent<Collider>().enabled = false;
        asrc.PlayOneShot(swallow);
        yield return new WaitForSeconds(deltaTime);
        isConsuming = false;
        FindTarget();
        GetComponent<Collider>().enabled = true;
    }
    // 碰到玩家后，玩家死亡
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (!other.GetComponent<PlayerMovement>().inShop)
            {
                other.GetComponent<PlayerMovement>().createDummy(new Vector3(125, 1.5f, 335));
                var clock = Instantiate(countDownCanvas, 
                    new Vector3(other.transform.position.x, other.transform.position.y + 10, other.transform.position.z),
                    Quaternion.identity, null);
                clock.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
                other.GetComponent<Collider>().isTrigger = true;
                Destroy(other.gameObject);
                StartCoroutine(ConsumePeople(2));
            }
            else FindTarget();
        }
        if (other.gameObject.layer == 10)
        {
            //吃掉食物
            other.GetComponent<Rigidbody>().useGravity = true;
            Destroy(other.gameObject, 2);
            StartCoroutine("ConsumeFood");
        }
        //摧毁障碍物
        if (other.gameObject.layer == 11)
        {
            other.gameObject.GetComponentInParent<Fall>().fall(transform.position);
            Destroy(other.gameObject, 2);
            asrc.PlayOneShot(swallow);
        }
    }

    float angleSpeed = 180f;
    float nearEnough = 5f;

    void RotateTowards()
    {
        Vector3 targetDirection = nearestTarget.transform.position - transform.position;
        float idealAngle = Mathf.Rad2Deg * Mathf.Atan2(targetDirection.x, targetDirection.z);
        float currentAngle = transform.rotation.eulerAngles.y;

        if (Mathf.Abs(Mathf.DeltaAngle(idealAngle, currentAngle)) > nearEnough)
        {
            float nextAngle = Mathf.MoveTowardsAngle(currentAngle, idealAngle, angleSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, nextAngle, 0));
        }
        else
            rotating = false;
    }


    private void Update()
    {
        if (CheckTimer > CheckTargetTime)
        {
            CheckTimer -= CheckTargetTime;
            FindTarget();
        }
        if (nearestTarget && rotating)
        {
            //if (transform.forward.normalized == (nearestTarget.transform.position - transform.position).normalized)
            //    rotating = true;
            //transform.forward += (nearestTarget.transform.position - transform.position).normalized * 20 * Time.fixedDeltaTime;
            RotateTowards();
        }
    }
    void FixedUpdate()
    {
        CheckTimer += Time.fixedDeltaTime;
        if (nearestTarget && !isConsuming && !rotating)
        {
            Vector3 direction = (nearestTarget.transform.position - transform.position).normalized;
            Debug.DrawRay(transform.position, direction * 30, Color.blue);
            //追踪
            rb.position = (rb.position + direction * MoveSpeed * Time.fixedDeltaTime);
            transform.LookAt(transform.position + direction);
        }
    }
}
