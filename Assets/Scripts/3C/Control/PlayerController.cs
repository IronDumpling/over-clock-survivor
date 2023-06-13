using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private DanmakuManager danmakuMng;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        danmakuMng = transform.parent.Find("PlayerDanmaku").GetComponent<DanmakuManager>();
    }

    private void Update()
    {
        danmakuMng.UseCommand(transform.position);
    }

    public void HandleMovement(float horizontal, float vertical)
    {
        rb.velocity = new Vector2(horizontal, vertical) * moveSpeed;
    }
}

