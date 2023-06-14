using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


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
        GameObject danmaku = Instantiate(attackPrefab, position, Quaternion.identity);
        danmaku.transform.SetParent(gameObject.transform);
        danmaku.transform.localScale *= c.scaleMultiTimes;
        danmaku.GetComponent<Danmaku>().dmg = c.dmg;
    }
}





