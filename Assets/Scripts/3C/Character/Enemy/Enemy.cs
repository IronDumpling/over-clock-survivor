using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour, IDanInteractable
{
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private GameObject particlePrefab;

    private float _currHealth;
    public float m_currHealth
    {
        get => _currHealth;
        private set
        {
            _currHealth = Mathf.Min(value, _fullHealth);
            _currHealth = Mathf.Max(value, 0f);
            enemyData.currHealth = _currHealth;
            onHealthChange.Invoke(m_currHealth);
        }
    }

    private float _fullHealth;
    public float m_fullHealth
    {
        get => _fullHealth;
        private set
        {
            _fullHealth = value;
            enemyData.fullHealth = _fullHealth;
        }
    }

    private float _dmg;
    public float m_dmg
    {
        get => _dmg;
        private set
        {
            _dmg = value;
            enemyData.dmg = _dmg;
        }
    }

    private float _moveSpeed;
    public float m_moveSpeed
    {
        get => _moveSpeed;
        private set
        {
            _moveSpeed = value;
            enemyData.moveSpeed = _moveSpeed;
        }
    }

    private string _target;
    public string m_target
    {
        get => _target;
        private set
        {
            _target = value;
            enemyData.target = _target;
        }
    }

    const int BULLET_LAYER = 6;

    public FloatChangeEvent onHealthChange;

    private void Awake()
    {
        Birth();
        gameObject.GetComponent<AIDestinationSetter>().target = GameObject.Find(m_target).transform;
        onHealthChange.AddListener(OnHealthChange);
    }

    private void Birth()
    {
        m_currHealth = enemyData.currHealth;
        m_fullHealth = enemyData.fullHealth;
        m_dmg = enemyData.dmg;
        m_moveSpeed = enemyData.moveSpeed;
        m_target = enemyData.target;
    }

    private void CurrHealthDown(float dmg)
    {
        if (dmg < 0) return;
        m_currHealth -= dmg;
    }

    private void CurrHealthUp(float recover)
    {
        if (recover < 0) return;
        m_currHealth += recover;
    }

    private void OnHealthChange(float currHealth)
    {
        if (currHealth <= 0) Death();
    }

    private void Death()
    {
        SpawnEnergyParticle();
        EnemyManager.Instance.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void SpawnEnergyParticle()
    {
        Transform particleParent = EnemyManager.Instance.particleParent;
        for (int i = 0; i < enemyData.energy.quantity; i++)
        {
            GameObject particle = Instantiate(particlePrefab, RandomPosition(), Quaternion.identity);
            EnergyParticle energy = particle?.GetComponent<EnergyParticle>();
            particle.transform.SetParent(particleParent);
            particle.name = $"Energy_Particle_{i}";

            energy.m_amount = enemyData.energy.amount;
            energy.m_color = enemyData.energy.color;
            energy.m_sprite = enemyData.energy.sprite;
            energy.Render();
        }
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-2f, 2f);
        float y = Random.Range(-2f, 2f);
        return new Vector3(x + transform.position.x, y + transform.position.y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == BULLET_LAYER)
        {
            // TODO: Bullet Would Collide Multiple Times
            CurrHealthDown(collision.gameObject.GetComponent<Danmaku>().dmg);
        }
    }
}
