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

    private void Death()
    {
        SpawnEnergyParticle();
        EnemyManager.Instance.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void SpawnEnergyParticle()
    {
        for (int i = 0; i < enemyData.energy.quantity; i++)
        {
            GameObject particle = Instantiate(particlePrefab, RandomPosition(), Quaternion.identity);
            EnergyParticle energy = particle?.GetComponent<EnergyParticle>();
            energy.energyAmount = enemyData.energy.amount;
            energy.color = enemyData.energy.color;
            energy.sprite = enemyData.energy.sprite;
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
            m_currHealth -= collision.gameObject.GetComponent<Danmaku>().dmg;
            if (m_currHealth <= 0) Death();
        }
    }
}
