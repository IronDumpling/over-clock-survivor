using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class Player : MonoSingleton<Player>, IDanInteractable
{
    [SerializeField] private float moveSpeed = 5f;
    private DanmakuManager danmakuMng;
    private Rigidbody2D rb;

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
