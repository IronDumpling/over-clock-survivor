using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DanmakuManager : MonoBehaviour
{
    [SerializeField] private GameObject attackPrefab;

    public void UseCommand(Vector3 position)
    {
        CellCommand c = CellManager.Instance.ComsumeCommand();

        if (c != null)
        {
            switch (c.type)
            {
                case CellType.Attack:

                    for (int i = 0; i < c.multiTimes; i++)
                    {
                        SpawnDanmaku(c, position);
                    }

                    break;
            }
        }
    }

    private void SpawnDanmaku(CellCommand c, Vector3 position)
    {
        GameObject closestEnemy = EnemyManager.Instance.GetClosestEnemy();

        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomEuler = new Vector3(0, 0, randomAngle);

        GameObject go = Instantiate(attackPrefab, position, Quaternion.Euler(randomEuler));

        go.transform.SetParent(gameObject.transform);
        go.transform.localScale *= c.scaleMultiTimes;
        go.GetComponent<Danmaku>().dmg = c.dmg;
    }
}
