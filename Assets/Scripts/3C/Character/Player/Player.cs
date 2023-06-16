using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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

    public NumChangeEvent onEnergyChange;
    public NumChangeEvent onLevelChange;
    public NumChangeEvent onHealthChange;
    public NumChangeEvent onFreqChange;

    protected override void Init()
    {
        base.Init();

        rb = GetComponent<Rigidbody2D>();
        danmakuMng = transform.parent.Find("PlayerDanmaku").GetComponent<DanmakuManager>();

        onEnergyChange.AddListener(OnEnergyChange);
        onHealthChange.AddListener(OnHealthChange);
        onFreqChange.AddListener(OnFreqChange);
        onLevelChange.AddListener(OnLevelChange);

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
        float maxEnergy;
        if(playerData.voltage.energyLimits.TryGetElement(m_level, out maxEnergy))
        {
            m_energy = Mathf.Min(m_energy + energy, maxEnergy);
            playerData.voltage.energy = m_energy;
            onEnergyChange.Invoke(m_energy);
            // TODO: Add Frequency Up Here
            // FreqUp();
        }
    }

    private void LoseEnergy(float energy)
    {
        m_energy = Mathf.Max(m_energy - energy, 0f);
        playerData.voltage.energy = m_energy;
        onEnergyChange.Invoke(m_energy);
    }

    private void CleanEnergy()
    {
        m_energy = 0f;
        playerData.voltage.energy = m_energy;
        onEnergyChange.Invoke(m_energy);
    }

    private void LevelUpTo(int level)
    {
        m_level = level;
        playerData.voltage.level = m_level;
        OverClock();
        CleanEnergy();
        FullHealthUp();
        onLevelChange.Invoke(m_level);
    }

    private void OnEnergyChange(float currEnergy)
    {
        float maxEnergy;
        if (playerData.voltage.energyLimits.TryGetElement(m_level, out maxEnergy))
        {
            if (currEnergy >= maxEnergy)
                LevelUpTo(m_level + 1);
        }
    }

    private void OnLevelChange(float currLevel)
    {

    }

    #endregion

    #region Frequency

    private void FreqDown(float freq)
    {
        float minFreq;
        if (playerData.frequency.lowerBounds.TryGetElement(m_level, out minFreq))
        {
            m_freq = Mathf.Max(m_freq - freq * Time.deltaTime, minFreq);
            playerData.frequency.currFreq = m_freq;
            onFreqChange.Invoke(m_freq);
        }
    }

    private void FreqUp(float freq)
    {
        float maxFreq;
        if (playerData.frequency.upperBounds.TryGetElement(m_level, out maxFreq))
        {
            m_freq = Mathf.Min(m_freq + freq, maxFreq);
            playerData.frequency.currFreq = m_freq;
            onFreqChange.Invoke(m_freq);
        }
    }

    private void OverClock()
    {
        float maxFreq;
        if (playerData.frequency.upperBounds.TryGetElement(m_level, out maxFreq))
        {
            m_freq = maxFreq;
            playerData.frequency.currFreq = m_freq;
            onFreqChange.Invoke(m_freq);
        }
    }

    private void OnFreqChange(float currFreq)
    {
        
    }

    #endregion

    #region Health

    private void CurrHealthUp(float recover)
    {
        m_currHealth = Mathf.Min(m_currHealth + recover, m_fullHealth);
        playerData.currHealth = m_currHealth;
        onHealthChange.Invoke(m_currHealth);
    }

    private void CurrHealthDown(float dmg)
    {
        m_currHealth = Mathf.Max(m_currHealth - dmg, 0f);
        playerData.currHealth = m_currHealth;
        onHealthChange.Invoke(m_currHealth);
    }

    private void FullHealthUp()
    {
        if(playerData.fullHealthList.TryGetElement(m_level, out m_fullHealth))
        {
            m_currHealth = m_fullHealth;
            playerData.currHealth = m_currHealth;
            onHealthChange.Invoke(m_currHealth);
        }

    }

    private void Death()
    {
        GameManager.Instance.RestartLevel();
    }

    private void OnHealthChange(float currHealth)
    {
        if (currHealth <= 0) Death();
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
        }
        else if (collision.gameObject.layer == ENERGY_PARTICLE_LAYER)
        {
            EnergyParticle energy = collision.gameObject?.GetComponent<EnergyParticle>();
            GainEnergy(energy.m_amount);
            energy.Death();
        }
    }
}

[System.Serializable]
public class NumChangeEvent : UnityEvent<float> { }
