using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityAOE { YES, NO }
public enum AbilityType { ATTACK, HEAL, SUPPORT, BUFF}

[CreateAssetMenu(fileName = "New Abilty", menuName = "ability")]
public class AbilityData : ScriptableObject
{
    public string abilityName;

    public string abilityElementType;
    public AbilityAOE abilityAOE;
    public AbilityType abilityType;

    public int abilityDamage;
    public int abilityHealCapacity;

    public float strongAgainstElementMultiplier;
    public float weakToElementModifier;
    public float abilityBuffMultiplier;
}
