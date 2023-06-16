using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour, IDanInteractable
{
    public EnemySO enemyData;
    private float m_currHealth;
    private float m_fullHealth;
    private float m_dmg;
    private float m_moveSpeed;
    private string m_target;

    [SerializeField] private GameObject particlePrefab;

    const int BULLET_LAYER = 6;

    private void Awake()
    {
        Birth();
        gameObject.GetComponent<AIDestinationSetter>().target = GameObject.Find(m_target).transform;
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
        m_currHealth = Mathf.Max(m_currHealth - dmg, 0f);
    }

    private void CurrHealthUp(float recover)
    {
        m_currHealth = Mathf.Min(m_currHealth + recover, m_fullHealth);
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
            if (m_currHealth <= 0) Death();
        }
    }
}
