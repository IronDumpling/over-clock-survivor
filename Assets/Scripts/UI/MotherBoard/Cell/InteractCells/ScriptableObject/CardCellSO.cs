using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCellSO : ScriptableObject
{
    public string title = "New Card Cell";
    public string description;
    public Sprite thumbnail;

    public CardShape shape;
    public CellType type;

    public int floodingTimes = 1;
}

[System.Serializable]
public enum CardShape
{
    L4, L5,
    T4, T5,
    I4,
    O4, O6, O8,
    Z4
}

[System.Serializable]
public enum CellType
{
    Attack,
    Shield,
    Support,
    Recover,
    Move,
    Resource,
    None
}

