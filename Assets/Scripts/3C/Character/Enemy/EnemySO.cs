using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Character/Enemy")]
public class EnemySO : CharacterSO
{
    public float fullHealth;

    public string enemyName;
    public Sprite enemySprite;

    public string target;
    public float dmg;

    public EnergyParticles energy;

    [System.Serializable]
    public struct EnergyParticles
    {
        /// <summary>
        /// The amount of energy of each energy particle
        /// </summary>
        public float energyAmount;
        /// <summary>
        /// The amount of frequency rise when player obtain the particle 
        /// </summary>
        public float freqAmount;
        /// <summary>
        /// The quantity of particle that would be spawned
        /// </summary>
        public int quantity;
        public Color color;
        public Sprite sprite;
    }
}


