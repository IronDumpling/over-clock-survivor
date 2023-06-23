using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Character/Player")]
public class PlayerSO : CharacterSO
{
    public Speed speed;
    public Health health;
    public Voltage voltage;
    public Frequency frequency;
}

[System.Serializable]
public struct Speed
{
    public float currSpeed;
    public float minSpeed;
    public float maxSpeed;
}

[System.Serializable]
public struct Health
{
    public float currHealth;
    public List<float> fullHealthList;
}

[System.Serializable]
public struct Voltage
{
    /// <summary>
    /// Current voltage level
    /// </summary>
    public int level;
    /// <summary>
    /// Current energy or experience amount,
    /// increase energy to level up voltage
    /// </summary>
    public float energy;
    /// <summary>
    /// A list of energy limit of each level
    /// </summary>
    public List<float> energyLimits;
}

[System.Serializable]
public struct Frequency
{
    /// <summary>
    /// Current frequency amount
    /// </summary>
    public float currFreq;
    public float naturalFreqDrop;
    /// <summary>
    /// A list of the upper and lower frequency bounds,
    /// of the current voltage level
    /// </summary>
    public List<float> lowerBounds;
    public List<float> upperBounds;
}
