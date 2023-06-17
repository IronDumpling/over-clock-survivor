using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Character/Player")]
public class PlayerSO : CharacterSO
{
    public float naturalFreqDrop;
    public List<float> fullHealthList;
    public Voltage voltage;
    public Frequency frequency;
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
    /// <summary>
    /// A list of the upper and lower frequency bounds,
    /// of the current voltage level
    /// </summary>
    public List<float> lowerBounds;
    public List<float> upperBounds;
}
