using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Character/Enemy")]
public class EnemySO : CharacterSO
{
    public string enemyName;
    public float dmg;
    public string target;
}
