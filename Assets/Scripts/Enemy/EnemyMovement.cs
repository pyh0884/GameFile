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
                    if (!nearestTarget)
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
                //TODO:玩家死亡效果
                other.GetComponent<PlayerMovement>().createDummy(new Vector3(125, 1.5f, 335));
                var clock = Instantiate(countDownCanvas, 
                    new Vector3(other.transform.position.x, other.transform.position.y + 10, other.transform.position.z),
                    Quaternion.identity, null);
                clock.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
                Destroy(other.GetComponent<PlayerMovement>());
                other.GetComponent<Collider>().isTrigger = true;
                Destroy(other.gameObject, 2);
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
    private void Update()
    {
        if (CheckTimer > CheckTargetTime)
        {
            CheckTimer -= CheckTargetTime;
            FindTarget();
        }

    }
    void FixedUpdate()
    {
        CheckTimer += Time.fixedDeltaTime;
        if (nearestTarget && !isConsuming)
        {
            Vector3 direction = (nearestTarget.transform.position - transform.position).normalized;
            //追踪
            rb.position = (rb.position + direction * MoveSpeed * Time.fixedDeltaTime);
        }
    }
}
