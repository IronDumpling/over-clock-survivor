using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : MonoSingleton<Player>, IDanInteractable
{
    [SerializeField] private PlayerSO playerData;

    private int _level;
    public int m_level
    {
        get => _level;
        private set
        {
            _level = Mathf.Min(value, _maxLevel);
            _level = Mathf.Max(value, 0);
            Debug.Log($"bounded level: {_level}");
            playerData.voltage.level = _level;
            onLevelChange.Invoke(_level);
        }
    }

    private int _maxLevel;

    private float _energy;
    public float m_energy
    {
        get => _energy;
        private set
        {
            _energy = Mathf.Min(value, _maxEnergy);
            _energy = Mathf.Max(value, 0f);
            playerData.voltage.energy = _energy;
            onEnergyChange.Invoke(_energy, _maxEnergy);
        }
    }

    private float _maxEnergy;

    private float _freq;
    public float m_freq
    {
        get => _freq;
        private set
        {
            _freq = Mathf.Min(value, _maxFreq);
            _freq = Mathf.Max(value, _minFreq);
            playerData.frequency.currFreq = m_freq;
            onFreqChange.Invoke(_freq, _minFreq, _maxFreq, _fullFreq);
        }
    }

    private float _minFreq;
    private float _maxFreq; // max freq means the upper bound of current level's freq
    private float _fullFreq; // full freq means the largest frequency in the game
    private float _naturalFreqDrop;

    private float _moveSpeed;
    public float m_moveSpeed
    {
        get => _moveSpeed;
        private set
        {
            _moveSpeed = Mathf.Max(value, 1f);
            playerData.moveSpeed = _moveSpeed;
        }
    }

    private float _currHealth;
    public float m_currHealth
    {
        get => _currHealth;
        private set
        {
            _currHealth = Mathf.Min(value, _fullHealth);
            _currHealth = Mathf.Max(value, 0f);
            playerData.currHealth = _currHealth;
            onHealthChange.Invoke(_currHealth, _fullHealth);
        }
    }

    private float _fullHealth;

    private DanmakuManager danmakuMng;
    private Rigidbody2D rb;

    const int ENEMY_LAYER = 8;
    const int ENERGY_PARTICLE_LAYER = 9;

    public IntChangeEvent onLevelChange;
    public TwoFloatChangeEvent onEnergyChange;
    public TwoFloatChangeEvent onHealthChange;
    public FourFloatChangeEvent onFreqChange;

    protected override void Init()
    {
        base.Init();

        rb = GetComponent<Rigidbody2D>();
        danmakuMng = transform.parent.Find("PlayerDanmaku").GetComponent<DanmakuManager>();

        onEnergyChange.AddListener(OnEnergyChange);
        onHealthChange.AddListener(OnHealthChange);
        onFreqChange.AddListener(OnFreqChange);
        onLevelChange.AddListener(OnLevelChange);
    }

    private void Start()
    {
        Birth();
    }

    private void Update()
    {
        danmakuMng.UseCommand(transform.position);
        FreqDown(_naturalFreqDrop);
    }

    void UpdateBounds()
    {
        playerData.frequency.lowerBounds.TryGetElement(m_level, out _minFreq);
        playerData.frequency.upperBounds.TryGetElement(m_level, out _maxFreq);
        playerData.voltage.energyLimits.TryGetElement(m_level, out _maxEnergy);
        playerData.fullHealthList.TryGetElement(m_level, out _fullHealth);
        Debug.Log("Update Bounds!");
    }

    void GetFixedBound()
    {
        _maxLevel = playerData.voltage.energyLimits.Count;
        playerData.frequency.upperBounds.TryGetElement(_maxLevel - 1, out _fullFreq);
    }

    private void Birth()
    {
        GetFixedBound();
        Debug.Log($"max level: {_maxLevel}");
        m_level = playerData.voltage.level;
        m_energy = playerData.voltage.energy;
        m_moveSpeed = playerData.moveSpeed;
        m_freq = _maxFreq;
        m_currHealth = _fullHealth;
        _naturalFreqDrop = playerData.naturalFreqDrop;
    }

    #region Voltage

    public void GainEnergy(float energy)
    {
        if (energy < 0) return;
        m_energy += energy;
    }

    public void LoseEnergy(float energy)
    {
        if (energy < 0) return;
        m_energy -= energy;
    }

    public void CleanEnergy()
    {
        m_energy = 0f;
    }

    private void OnEnergyChange(float currEnergy, float maxEnergy)
    {
        if (currEnergy >= maxEnergy)
        {
            Debug.Log("Level Up!");
            LevelUpTo(m_level + 1);
        }
    }

    public void LevelUpTo(int level)
    {
        Debug.Log($"Try to level Up to {level}");
        m_level = level;
    }

    private void OnLevelChange(int currLevel)
    {
        UpdateBounds();
        OverClock();
        CleanEnergy();
        CurrHealthFull();
    }

    #endregion

    #region Frequency

    private void FreqDown(float freq)
    {
        if (freq < 0) return;
        m_freq -= freq * Time.deltaTime;
    }

    private void FreqUp(float freq)
    {
        if (freq < 0) return;
        m_freq += freq;
    }

    private void OverClock()
    {
        m_freq = _maxFreq;
    }

    private void OnFreqChange(float currFreq, float minFreq, float maxFreq, float fullFreq)
    {
        
    }

    #endregion

    #region Health

    private void CurrHealthUp(float recover)
    {
        if (recover < 0) return;
        m_currHealth += recover;       
    }

    private void CurrHealthDown(float dmg)
    {
        if (dmg < 0) return;
        m_currHealth -= dmg;
    }

    private void CurrHealthFull()
    { 
        m_currHealth = _fullHealth;
    }

    private void Death()
    {
        GameManager.Instance.RestartLevel();
    }

    private void OnHealthChange(float currHealth, float fullHealth)
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
            CurrHealthDown(collision.gameObject.GetComponent<Enemy>().m_dmg);
        }
        else if (collision.gameObject.layer == ENERGY_PARTICLE_LAYER)
        {
            EnergyParticle energy = collision.gameObject?.GetComponent<EnergyParticle>();
            GainEnergy(energy.m_energy);
            FreqUp(energy.m_freq);
            energy.Death();
        }
    }
}
