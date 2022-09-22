using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector] public string unitName;
    [HideInInspector] public int maxHp, currentHp;
    [HideInInspector] public int maxMana, currentMana;
    [HideInInspector] public int speed;
    [HideInInspector] public int attackDamage;

    [HideInInspector] public string unitElement;
    [HideInInspector] public string unitWeakToElement;
    [HideInInspector] public string unitStrongAgainstElement;
    [HideInInspector] public float unitWeakToElementModifier;
    [HideInInspector] public float unitStrongAgainstModifier;

    [SerializeField] UnitData unitData;
    [SerializeField] ElementData elementData;

    private void Awake()
    {
        maxHp = unitData.maxHp;
        maxMana = unitData.maxMana;
        speed = unitData.speed;
        attackDamage = unitData.attackDamage;
        unitName = unitData.name;

        unitElement = elementData.elementName;
        unitWeakToElement = elementData.weakAgainstElement;
        unitStrongAgainstElement = elementData.strongAgainstElement;
        unitWeakToElementModifier = elementData.weakToElementModifier;
        unitStrongAgainstModifier = elementData.strongAgainstElementModifier;
    }

    private void Start()
    {
        currentHp = maxHp;
        currentMana = maxMana;
    }

    private void Update()
    {
        
    }

    public bool TakeDamage(int dmg)
    {
        currentHp -= dmg;

        if (currentHp <= 0)
            return true;
        else
            return false;
    }
}
