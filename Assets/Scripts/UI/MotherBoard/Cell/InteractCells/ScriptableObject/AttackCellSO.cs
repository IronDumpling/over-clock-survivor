using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Cell", menuName = "Card Cell/Attack Cell")]
public class AttackCellSO : CardCellSO
{
    public GenDanmaku danmaku;
}

[System.Serializable]
public struct GenDanmaku
{
    public DanmakuShape shape;
    public DanmakuMove move;
    public float speed;
    public float diameter;
    public float dmgAmount;
    public float dmgTimes;
}

[System.Serializable]
public enum DanmakuShape
{
    dot, path, area
}

[System.Serializable]
public enum DanmakuMove
{
    ChooseClosestEnemy,
    RotateAroundPlayer,
    RandomGenerate,
    AtPlayerPosition
}