using System.Collections;
using System.Collections.Generic;
using System;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : MonoSingleton<Player>
{
	[SerializeField] private PlayerSO playerData;

	private int _level;
	public int m_level
	{
		get => _level;
		private set
		{
			_level = Mathf.Min(value, _maxLevel);
			_level = Mathf.Max(_level, 0);
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
			_energy = Mathf.Max(_energy, 0f);
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
			_freq = Mathf.Max(_freq, _minFreq);
			playerData.frequency.currFreq = m_freq;
			onFreqChange.Invoke(_freq, _minFreq, _maxFreq, _fullFreq);
		}
	}

	private float _minFreq;
	private float _maxFreq; // max freq means the upper bound of current level's freq
	private float _fullFreq; // full freq means the largest frequency in the game
	private float _naturalFreqDrop;

	private float _currSpeed;
	public float m_currSpeed
	{
		get => _currSpeed;
		private set
		{
			_currSpeed = Mathf.Max(value, _minSpeed);
			_currSpeed = Mathf.Min(_currSpeed, _maxSpeed);
			_currSpeed = (float)Math.Round(_currSpeed, 1);
			playerData.speed.currSpeed = _currSpeed;
		}
	}

	private float _minSpeed;
	private float _maxSpeed;

	private float _currHealth;
	public float m_currHealth
	{
		get => _currHealth;
		private set
		{
			_currHealth = Mathf.Min(value, _fullHealth);
			_currHealth = Mathf.Max(_currHealth, 0f);
			playerData.health.currHealth = _currHealth;
			onHealthChange.Invoke(_currHealth, _fullHealth);
		}
	}

	private float _fullHealth;

	private DanmakuManager danmakuMng;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRender;

	public IntChangeEvent onLevelChange;
	public TwoFloatChangeEvent onEnergyChange;
	public TwoFloatChangeEvent onHealthChange;
	public FourFloatChangeEvent onFreqChange;

	protected override void Init()
	{
		base.Init();

		rb = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
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
		if (playerData.frequency.lowerBounds.TryGetElement(m_level, out float minFreq)) _minFreq = minFreq;
		if (playerData.frequency.upperBounds.TryGetElement(m_level, out float maxFreq)) _maxFreq = maxFreq;
		if (playerData.voltage.energyLimits.TryGetElement(m_level, out float maxEnergy)) _maxEnergy = maxEnergy;
		if (playerData.health.fullHealthList.TryGetElement(m_level, out float fullHealth)) _fullHealth = fullHealth;
	}

	void GetFixedBound()
	{
		_maxLevel = playerData.voltage.energyLimits.Count - 1;
        
		_minSpeed = playerData.speed.minSpeed;
        _maxSpeed = playerData.speed.maxSpeed;

        _naturalFreqDrop = playerData.frequency.naturalFreqDrop;

		if (playerData.frequency.upperBounds.TryGetElement(_maxLevel, out float fullFreq)) _fullFreq = fullFreq;
	}

	private void Birth()
	{
		GetFixedBound();

		m_level = playerData.voltage.level;

		m_energy = playerData.voltage.energy;

		m_freq = _maxFreq;

		m_currHealth = _fullHealth;

		MapFreqToSpeed();
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
			LevelUpTo(m_level + 1);
		}
	}

	public void LevelUpTo(int level)
	{
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
		MapFreqToSpeed();
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

	private void MapFreqToSpeed()
	{
		float speedRange = _maxSpeed - _minSpeed;

		float normFreq = Mathf.Clamp01(m_freq / _fullFreq);

		m_currSpeed = (normFreq * speedRange) + _minSpeed;
	}

	public void HandleMovement(float horizontal, float vertical)
	{
		rb.velocity = new Vector2(horizontal, vertical) * m_currSpeed;
		SpriteFlip(horizontal);
	}

	#endregion

	#region Render

	private void SpriteFlip(float horizontal)
	{
		if (horizontal < 0) spriteRender.flipX = true;
		else if (horizontal > 0) spriteRender.flipX = false;
	}

	#endregion

	#region Interaction

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerUtil.ENERGY_PARTICLE_LAYER)
		{
			CollideEnergyParticle(collision.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.gameObject.layer)
		{
			case LayerUtil.ENEMY_LAYER:
				CollideEnemy(collision.gameObject);
				break;
			case LayerUtil.OBSTACLE_LAYER:
				CollideObstacle(collision.gameObject);
				break;
			default:
				break;
		}
	}

	private void CollideEnemy(GameObject enemy)
	{
		CurrHealthDown(enemy.GetComponent<Enemy>().m_dmg);
	}

	private void CollideDanmaku(GameObject danmaku)
	{

	}

	private void CollideEnergyParticle(GameObject particle)
	{
		EnergyParticle energy = particle.GetComponent<EnergyParticle>();
		GainEnergy(energy.m_energy);
		FreqUp(energy.m_freq);
		energy.Death();
	}

	private void CollideObstacle(GameObject obstacle)
	{

	}

	private void CollideTrap(GameObject trap)
	{

	}

	#endregion
}
