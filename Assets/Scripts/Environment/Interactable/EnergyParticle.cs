using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyParticle : MonoBehaviour
{
    private float _energyAmount;
    public float energyAmount
    {
        get => _energyAmount;
        set => _energyAmount = value;
    }

    public Sprite sprite;
    public Color color;

    public void Death()
    {
        Destroy(gameObject);
    }
}
