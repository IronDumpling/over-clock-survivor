using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class Player : MonoSingleton<Player>, IDanInteractable
{
    public PlayerSO playerData;
    private int m_level;
    private float m_energy;
    private float m_freq;
    private float m_moveSpeed;
    private float m_currHealth;
    private float m_fullHealth;

    private DanmakuManager danmakuMng;
    private Rigidbody2D rb;

    const int ENEMY_LAYER = 8;
    const int ENERGY_PARTICLE_LAYER = 9;

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
        m_level = playerData.voltage.level;
        m_energy = playerData.voltage.energy;
        m_freq = playerData.frequency.currFreq;
        m_moveSpeed = playerData.moveSpeed;
        m_currHealth = playerData.currHealth;
        FullHealthUp();
    }

    #region Voltage

    private void GainEnergy(float energy)
    {
        m_energy = Mathf.Min(m_energy + energy,
                             playerData.voltage.energyLimits[m_level]);
        playerData.voltage.energy = m_energy;
    }

    private void CleanEnergy()
    {
        m_energy = 0f;
        playerData.voltage.energy = m_energy;
    }

    private void LevelUpTo(int level)
    {
        m_level = level;
        playerData.voltage.level = m_level;
        OverClock();
        CleanEnergy();
        FullHealthUp();
    }

    #endregion

    #region Frequency

    private void FreqDown(float freq)
    {
        m_freq = Mathf.Max(m_freq - freq * Time.deltaTime,
                           playerData.frequency.lowerBounds[m_level]);
        playerData.frequency.currFreq = m_freq;
    }

    private void FreqUp(float freq)
    {
        m_freq = Mathf.Min(m_freq + freq,
                           playerData.frequency.upperBounds[m_level]);
        playerData.frequency.currFreq = m_freq;
    }

    private void OverClock()
    {
        m_freq = playerData.frequency.upperBounds[m_level];
    }

    #endregion

    #region Health

    private void CurrHealthUp(float recover)
    {
        m_currHealth = Mathf.Min(m_currHealth + recover, m_fullHealth);
        playerData.currHealth = m_currHealth;
    }

    private void CurrHealthDown(float dmg)
    {
        m_currHealth = Mathf.Max(m_currHealth - dmg, 0f);
        playerData.currHealth = m_currHealth;
    }

    private void FullHealthUp()
    {
        m_fullHealth = playerData.fullHealthList[m_level];
    }

    private void Death()
    {
        GameManager.Instance.RestartLevel();
    }

    #endregion

    #region Movement

    public void HandleMovement(float horizontal, float vertical)
    {
        rb.velocity = new Vector2(horizontal, vertical) * m_moveSpeed;
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == ENEMY_LAYER)
        {
            CurrHealthDown(collision.gameObject.GetComponent<Enemy>().enemyData.dmg);
            if (m_currHealth <= 0) Death();
        }
        else if (collision.gameObject.layer == ENERGY_PARTICLE_LAYER)
        {
            EnergyParticle energy = collision.gameObject?.GetComponent<EnergyParticle>();
            GainEnergy(energy.m_amount);
            energy.Death();
        }
    }
}
