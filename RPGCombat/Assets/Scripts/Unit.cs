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

    public AbilityData abilityOne;
    public AbilityData abilityTwo;
    public AbilityData abilityThree;
    public AbilityData abilityFour;

    private BattleHUD battleHUD;

    private void Awake()
    {
        battleHUD = FindObjectOfType<BattleHUD>();

        maxHp = unitData.maxHp;
        maxMana = unitData.maxMana;
        speed = unitData.speed;
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

    public void Heal(int healCapacity)
    {
        currentHp += healCapacity;

        if(currentHp > maxHp)
            currentHp = maxHp;

        battleHUD.SetHpAndMana(currentHp, currentMana);
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
