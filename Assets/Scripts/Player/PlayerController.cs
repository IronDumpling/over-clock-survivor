using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private GameObject attackPrefab;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UseCommand();
    }

    private void UseCommand()
    {
        EComponentCommand c = EComponentManager.Instance.ComsumeCommand();
        if (c != null)
        {
            switch (c.type)
            {
                case EComponentType.Attack:

                    for (int i = 0; i < c.multiTimes; i++)
                    {
                        float randomAngle = Random.Range(0f, 360f);
                        Vector3 randomEuler = new Vector3(0, 0,randomAngle);
                        GameObject go  = Instantiate(attackPrefab, transform.position, Quaternion.Euler(randomEuler));
                        go.transform.localScale *= c.scaleMultiTimes;
                        go.GetComponent<Danmaku>().dmg = c.dmg;
                    }

                    //
                    break;
            }
        }
    }
    


    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        HandleMovement(horizontal, vertical);
    }

    private void HandleMovement(float horizontal, float vertical)
    {
        rb.velocity = new Vector2(horizontal, vertical) * moveSpeed;
    }
}

