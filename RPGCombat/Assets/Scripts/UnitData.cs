using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "unit")]
public class UnitData : ScriptableObject
{
    public string name;
    public int maxHp;
    public int maxMana;
    public int speed;
}
