using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    public float dmg;
    const int BULLET_LAYER = 6;

    private void Start()
    {
        _health = 10;
        dmg = 5;
        gameObject.GetComponent<AIDestinationSetter>().target = GameObject.Find("BattleStage/Player/PlayerSprite").transform;
    }

    private void Update()
    {
        if (_health <= 0) Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == BULLET_LAYER)
        {
            if(_health > 0)_health -= collision.gameObject.GetComponent<Danmaku>().dmg;
        }
    }

    private void Death()
    {
        EnemyManager.Instance.enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
