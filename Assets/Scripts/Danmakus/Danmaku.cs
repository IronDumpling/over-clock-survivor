using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Danmaku : MonoBehaviour
{
    public float dmg = 5;
    public float speed = 10f;

    private void Awake()
    {
        Invoke(nameof(DestroySelf), 5f);
        GetDirection();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetDirection()
    {
        GameObject closestEnemy = EnemyManager.Instance.GetClosestEnemy(transform);

        if (closestEnemy)
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            direction.Normalize();
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        else
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            randomDirection.Normalize();
            GetComponent<Rigidbody2D>().velocity = randomDirection * speed;
        }
    }
}
