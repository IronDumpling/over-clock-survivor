using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Danmaku : MonoBehaviour
{
    private float _dmg = 5;
    public float m_dmg
    {
        get => _dmg;
        set => _dmg = value;
    }

    private float speed = 10f;
    private float lifeSpan = 10f;

    private void Awake()
    {
        Birth();
    }

    #region Life Cycle

    void Birth()
    {
        Invoke(nameof(Death), lifeSpan);
        Movement();
    }

    void Death()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Movement

    void Movement()
    {
        EmitToClosest();
    }

    void EmitToClosest()
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
            RandomEmit();
        }
    }

    void RandomEmit()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        randomDirection.Normalize();
        GetComponent<Rigidbody2D>().velocity = randomDirection * speed;
    }

    void RandomGenerate()
    {

    }

    void RotateAroundPlayer()
    {

    }

    void GenerateAtPlayer()
    {

    }

    #endregion

    #region Interaction

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case LayerUtil.ENEMY_LAYER:
                CollideEnemy(collision.gameObject);
                break;
            case LayerUtil.OBSTACLE_LAYER:
                CollideObstacle(collision.gameObject);
                break;
            case LayerUtil.PLAYER_LAYER:
                CollidePlayer(collision.gameObject);
                break;
            default:
                break;
        }
    }

    private void CollideObstacle(GameObject obstacle)
    {
        Death();
    }

    private void CollideEnemy(GameObject enemy)
    {
        Death();
    }

    private void CollidePlayer(GameObject player)
    {
        
    }

    #endregion
}
