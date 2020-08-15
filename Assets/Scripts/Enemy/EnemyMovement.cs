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
    private Rigidbody rb;
    public float MoveSpeed;
    public float ConsumeTime = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
                if ((col.transform.position - transform.position).magnitude < (nearestTarget.transform.position - transform.position).magnitude)
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
                if (!nearestTarget)
                {
                    nearestTarget = col;
                }
                if ((col.transform.position - transform.position).magnitude < (nearestTarget.transform.position - transform.position).magnitude && !col.GetComponent<PlayerMovement>().inShop)
                {
                    nearestTarget = col;
                }
            }
        }
        return nearestTarget;
    }
    public IEnumerator ConsumeFood()
    {
        transform.localScale *= 2;
        nearestTarget = null;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(ConsumeTime);
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
                Destroy(other.gameObject);
                StartCoroutine("ConsumeFood");
                FindTarget();
            }
        }
        if (other.gameObject.layer == 10)
        {
            //吃掉食物
            Destroy(other.gameObject);
            StartCoroutine("ConsumeFood");
        }
        //摧毁障碍物
        if (other.gameObject.layer == 11)
        {
            Destroy(other.gameObject);
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
        if (nearestTarget)
        {
            Vector3 direction = (nearestTarget.transform.position - transform.position).normalized;
            //追踪
            rb.position = (rb.position + direction * MoveSpeed * Time.fixedDeltaTime);
        }
    }
}
