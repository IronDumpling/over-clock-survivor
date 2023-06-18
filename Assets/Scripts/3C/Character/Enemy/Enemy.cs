using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
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
            _currHealth = Mathf.Max(_currHealth, 0f);
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
        }
    }

    private float _dmg;
    public float m_dmg
    {
        get => _dmg;
        private set
        {
            _dmg = value;
        }
    }

    private float _moveSpeed;
    public float m_moveSpeed
    {
        get => _moveSpeed;
        private set
        {
            _moveSpeed = value;
            aiPath.maxSpeed = _moveSpeed;
        }
    }

    private string _target;
    public string m_target
    {
        get => _target;
        private set
        {
            _target = value;
            aiDest.target = GameObject.Find(_target)?.transform;
        }
    }

    private Sprite _sprite;
    public Sprite m_sprite
    {
        get => _sprite;
        private set
        {
            _sprite = value;
            spriteRender.sprite = _sprite;
        }
    }

    private AIPath aiPath;
    private AIDestinationSetter aiDest;
    private SpriteRenderer spriteRender;

    public FloatChangeEvent onHealthChange;

    private void Awake()
    {
        aiDest = gameObject.GetComponent<AIDestinationSetter>();
        aiPath = gameObject.GetComponent<AIPath>();
        spriteRender = gameObject.GetComponent<SpriteRenderer>();

        onHealthChange.AddListener(OnHealthChange);
    }

    private void Start()
    {
        Birth();
    }

    private void Birth()
    {
        m_fullHealth = enemyData.fullHealth;
        m_currHealth = enemyData.currHealth;
        m_dmg = enemyData.dmg;
        m_moveSpeed = enemyData.moveSpeed;
        m_target = enemyData.target;
        m_sprite = enemyData.enemySprite;
    }

    #region Health

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

    #endregion

    #region Energy Particle

    private void SpawnEnergyParticle()
    {
        Transform particleParent = EnemyManager.Instance.particleParent;
        for (int i = 0; i < enemyData.energy.quantity; i++)
        {
            GameObject particle = Instantiate(particlePrefab, RandomPosition(), Quaternion.identity);
            particle.transform.SetParent(particleParent);
            particle.name = $"Energy_Particle_{i}";

            EnergyParticle energy = particle?.GetComponent<EnergyParticle>();
            energy.m_energy = enemyData.energy.energyAmount;
            energy.m_freq = enemyData.energy.freqAmount;
            energy.m_color = enemyData.energy.color;
            energy.m_sprite = enemyData.energy.sprite;
            energy.Render();
        }
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-transform.localScale.x / 2f, transform.localScale.x / 2f);
        float y = Random.Range(-transform.localScale.x / 2f, transform.localScale.x / 2f);
        return new Vector3(x + transform.position.x, y + transform.position.y, 0f);
    }

    #endregion

    #region Interaction

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case LayerUtil.DANMAKU_LAYER:
                CollideDanmaku(collision.gameObject);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case LayerUtil.OBSTACLE_LAYER:
                CollideObstacle(collision.gameObject);
                break;
            case LayerUtil.PLAYER_LAYER:
                CollidePlayer(collision.gameObject);
                break;
            default:
                break;
        }
    }

    private void CollideDanmaku(GameObject danmaku)
    {
        CurrHealthDown(danmaku.GetComponent<Danmaku>().m_dmg);
    }

    private void CollidePlayer(GameObject player)
    {

    }

    private void CollideObstacle(GameObject obstacle)
    {

    }

    private void CollideTrap(GameObject trap)
    {

    }

    #endregion
}
