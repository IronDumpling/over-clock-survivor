using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : MonoSingleton<Player>, IDanInteractable
{
    private PlayerSO playerData;

    private int _level;
    public int m_level
    {
        get => _level;
        private set
        {
            _level = value;
            playerData.voltage.level = _level;
            onLevelChange.Invoke(_level);
        }
    }

    private float _energy;
    public float m_energy
    {
        get => _energy;
        private set
        {
            _energy = value;
            playerData.voltage.energy = _energy;
            onEnergyChange.Invoke(m_energy);
        }
    }

    private float _freq;
    public float m_freq
    {
        get => _freq;
        private set
        {
            _freq = value;
            playerData.frequency.currFreq = m_freq;
            onFreqChange.Invoke(m_freq);
        }
    }

    private float _moveSpeed;
    public float m_moveSpeed
    {
        get => _moveSpeed;
        private set
        {
            _moveSpeed = value;
            playerData.moveSpeed = _moveSpeed;
        }
    }

    private float _currHealth;
    public float m_currHealth
    {
        get => _currHealth;
        private set
        {
            _currHealth = value;
            playerData.currHealth = _currHealth;
            onHealthChange.Invoke(m_currHealth);
        }
    }

    private float _fullHealth;
    public float m_fullHealth
    {
        get => _fullHealth;
        private set => _fullHealth = value;
    }

    private DanmakuManager danmakuMng;
    private Rigidbody2D rb;

    const int ENEMY_LAYER = 8;
    const int ENERGY_PARTICLE_LAYER = 9;

    public IntChangeEvent onLevelChange;
    public FloatChangeEvent onEnergyChange;
    public FloatChangeEvent onHealthChange;
    public FloatChangeEvent onFreqChange;

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

    public void GainEnergy(float energy)
    {
        if (energy < 0) return;
        float maxEnergy;
        if(playerData.voltage.energyLimits.TryGetElement(m_level, out maxEnergy))
        {
            m_energy = Mathf.Min(m_energy + energy, maxEnergy);
            // TODO: Add Frequency Up Here
            // FreqUp();
        }
    }

    public void LoseEnergy(float energy)
    {
        if (energy < 0) return;
        m_energy = Mathf.Max(m_energy - energy, 0f);
    }

    public void CleanEnergy()
    {
        m_energy = 0f;
    }

    public void LevelUpTo(int level)
    {
        if (level < 0 || level > playerData.voltage.energyLimits.Count) return;
        m_level = level;
        OverClock();
        CleanEnergy();
        FullHealthUp();
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

    private void OnLevelChange(int currLevel)
    {

    }

    #endregion

    #region Frequency

    private void FreqDown(float freq)
    {
        if (freq < 0) return;
        float minFreq;
        if (playerData.frequency.lowerBounds.TryGetElement(m_level, out minFreq))
        {
            m_freq = Mathf.Max(m_freq - freq * Time.deltaTime, minFreq);
        }
    }

    private void FreqUp(float freq)
    {
        if (freq < 0) return;
        float maxFreq;
        if (playerData.frequency.upperBounds.TryGetElement(m_level, out maxFreq))
        {
            m_freq = Mathf.Min(m_freq + freq, maxFreq);
        }
    }

    private void OverClock()
    {
        float maxFreq;
        if (playerData.frequency.upperBounds.TryGetElement(m_level, out maxFreq))
        {
            m_freq = maxFreq;
        }
    }

    private void OnFreqChange(float currFreq)
    {
        
    }

    #endregion

    #region Health

    private void CurrHealthUp(float recover)
    {
        if (recover < 0) return;
        m_currHealth = Mathf.Min(m_currHealth + recover, m_fullHealth);       
    }

    private void CurrHealthDown(float dmg)
    {
        if (dmg < 0) return;
        m_currHealth = Mathf.Max(m_currHealth - dmg, 0f);
    }

    private void FullHealthUp()
    {
        float healthLimit;
        if(playerData.fullHealthList.TryGetElement(m_level, out healthLimit))
        {
            m_fullHealth = healthLimit;
            m_currHealth = m_fullHealth;
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
