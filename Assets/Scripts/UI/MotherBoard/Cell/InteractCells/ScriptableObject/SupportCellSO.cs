using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Support Cell", menuName = "Card Cell/Support Cell")]
public class SupportCellSO : CardCellSO
{
    public GenBUFF buff;
}

[System.Serializable]
public struct GenBUFF
{
    public BUFFType type;
    public BUFFLogic logic;
}

[System.Serializable]
public enum BUFFType
{
    Burn, // 灼烧
    Penetrate, // 穿透
    SlowDown, // 粘滞
    SuckBlood, // 吸血
    Split, // 分裂
    None
}

[System.Serializable]
public enum BUFFLogic
{
    DirectTouch,
    ExecuteAfter,
}
