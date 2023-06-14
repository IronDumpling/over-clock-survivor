using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoSingleton<Player>, IDanInteractable
{
    public PlayerSO playerData;
    private float m_moveSpeed;
    private float m_currHealth;
    private float m_fullHealth;

    private DanmakuManager danmakuMng;
    private Rigidbody2D rb;

    const int ENEMY_LAYER = 8;

    protected override void Init()
    {
        base.Init();

        rb = GetComponent<Rigidbody2D>();
        danmakuMng = transform.parent.Find("PlayerDanmaku").GetComponent<DanmakuManager>();

        Birth();
    }

    private void Update()
    {
        danmakuMng.UseCommand(transform.position);
    }

    private void Birth()
    {
        m_moveSpeed = playerData.moveSpeed;
        m_currHealth = playerData.currHealth;
        m_fullHealth = playerData.fullHealth;
    }

    private void Death()
    {
        GameManager.Instance.RestartLevel();
    }

    public void HandleMovement(float horizontal, float vertical)
    {
        rb.velocity = new Vector2(horizontal, vertical) * m_moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == ENEMY_LAYER)
        {
            m_currHealth -= collision.gameObject.GetComponent<Enemy>().enemyData.dmg;
            playerData.currHealth = m_currHealth;
            if (m_currHealth <= 0) Death();
        }
    }
}
