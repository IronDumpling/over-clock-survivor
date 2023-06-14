using System.Collections.Generic;
using UnityEngine;
using Common;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public float spawnInterval = 5f;
    private Timer spawnTimer;

    [SerializeField] private Transform Player;
    [SerializeField] private GameObject enemyPrefab;

    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = new Timer(spawnInterval, SpawnEnemy);
        spawnTimer.Start();
        Player = GameObject.Find("BattleStage/Player/PlayerSprite").transform;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer.Update(Time.deltaTime);
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-15f, 15f);
        float y = Random.Range(-15f, 15f);
        return new Vector3(x + Player.position.x, y + Player.position.y, 0f);
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = RandomPosition();
        GameObject spwanedEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        spwanedEnemy.transform.SetParent(gameObject.transform);
        enemies.Add(spwanedEnemy);
    }

    public GameObject GetClosestEnemy(Transform targetTrans)
    {
        GameObject closestObj = null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(targetTrans.position, enemy.transform.position);

            if(distance < closestDistance)
            {
                closestObj = enemy;
                closestDistance = distance;
            }
        }

        return closestObj;
    }
}
