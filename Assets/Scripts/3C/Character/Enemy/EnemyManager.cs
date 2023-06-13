using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    private Timer spawnTimer;
    public Transform Player; 

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = new Timer(spawnInterval, SpawnEnemy);
        spawnTimer.Start();
        Player = GameObject.Find("BattleStage/Player").transform;
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
    }
}

public class Timer
{
    private float interval;
    private float currentTime;
    private System.Action callback;

    public Timer(float interval, System.Action callback)
    {
        this.interval = interval;
        this.callback = callback;
    }

    public void Start()
    {
        currentTime = interval;
    }

    public void Update(float deltaTime)
    {
        currentTime -= deltaTime;
        if (currentTime <= 0f)
        {
            callback.Invoke();
            currentTime = interval;
        }
    }
}
