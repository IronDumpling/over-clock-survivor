using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCellSO : ScriptableObject
{
    public string cardTitle;
    public CardDescription cardDescription;
    public CardAmount cardCost;

    public CardAmount cardEffect;
    public CardAmount buffAmount;
    public Sprite cardIcon;

    public CardShape cardShape;
    public enum CardShape
    {
        L4, L5,
        T4, T5,
        I4, 
        O4, O6, O8,
        Z4
    }

    public CardType cardType;
    public enum CardType
    {
        Attack,
        Shield,
        Support,
        Recover,
        Move,
        Resource,
        None
    }

    public CardTargetType cardTargetType;
    public enum CardTargetType
    {
        self, enemy
    };

    public int GetCardCostAmount()
    {
        return cardCost.baseAmount; 
    }
    public int GetCardEffectAmount()
    {
         return cardEffect.baseAmount;
    }
    public string GetCardDescriptionAmount()
    {
        return cardDescription.baseAmount;
    }
    public int GetBuffAmount()
    {
         return buffAmount.baseAmount;
    }
}

[System.Serializable]
public struct CardAmount
{
    public int baseAmount;
}

[System.Serializable]
public struct CardDescription
{
    public string baseAmount;
}

[System.Serializable]
public struct CardBuffs
{
    //public Buff.Type buffType;
    public CardAmount buffAmount;
}

