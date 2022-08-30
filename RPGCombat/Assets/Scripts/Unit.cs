using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int maxHp, currentHp;
    private int maxMana, currentMana;
    private int speed;
    private int attackDamage;

    [SerializeField] UnitData unitData;

    private void Awake()
    {
        maxHp = unitData.maxHp;
        maxMana = unitData.maxMana;
        speed = unitData.speed;
        attackDamage = unitData.attackDamage;
    }

    private void Start()
    {
        currentHp = maxHp;
        currentMana = maxMana;
    }

    private void Update()
    {
        
    }
}
