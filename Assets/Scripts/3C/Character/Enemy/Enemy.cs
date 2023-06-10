using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    //Vector3 _targetPosition;
    public float _health;
    public float dmg;
    int bullet = 6;

    private void Start()
    {
        _health = 10;
        dmg = 5;
        gameObject.GetComponent<AIDestinationSetter>().target = GameObject.Find("BattleStage/Player").transform;
    }

    private void Update()
    {
        if (_health <= 0) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == bullet)
        {
            if(_health > 0)_health -= collision.gameObject.GetComponent<Danmaku>().dmg;
        }

        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().health -= dmg;
        }
    }
}
