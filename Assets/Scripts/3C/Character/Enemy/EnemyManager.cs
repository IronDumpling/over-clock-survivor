using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Common;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField] private GameObject enemyPrefab;
    
    private Timer spawnTimer;
    private GridGraph nav;
    private Transform enemyParent;
    public Transform particleParent;

    public float spawnInterval = 5f;
    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = new Timer(spawnInterval, SpawnEnemy);
        spawnTimer.Start();

        nav = (GridGraph)GetComponent<AstarPath>().graphs[0];
        transform.position = new Vector3(nav.center.x, nav.center.y, 0f);

        enemyParent = transform.Find("Enemies");
        particleParent = transform.Find("EnergyParticles");
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer.Update(Time.deltaTime);
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-nav.width/2f, nav.width/2f);
        float y = Random.Range(-nav.depth/2f, nav.depth/2f);
        return new Vector3(x + transform.position.x, y + transform.position.y, 0f);
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = RandomPosition();
        GameObject spwanedEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        spwanedEnemy.transform.SetParent(enemyParent);
        spwanedEnemy.name = $"Enemy_{enemies.Count}";
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
