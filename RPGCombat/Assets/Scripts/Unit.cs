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
    [HideInInspector] public Sprite elementSprite;

    [SerializeField] UnitData unitData;
    [SerializeField] ElementData elementData;

    public AbilityData abilityOne;
    public AbilityData abilityTwo;
    public AbilityData abilityThree;
    public AbilityData abilityFour;

    [HideInInspector] public BattleHUD battleHUD;
    [HideInInspector] public bool isUnitDead;
    private BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        isUnitDead = false;

        if(gameObject.transform.position == battleManager.playerTransform1.position)
        {
            battleHUD = battleManager.playerHUD1.GetComponent<BattleHUD>();
        }
        else if(gameObject.transform.position == battleManager.playerTransform2.position)
        {
            battleHUD = battleManager.playerHUD2.GetComponent<BattleHUD>();
        }
        else if (gameObject.transform.position == battleManager.playerTransform3.position)
        {
            battleHUD = battleManager.playerHUD3.GetComponent<BattleHUD>();
        }
        else if (gameObject.transform.position == battleManager.playerTransform4.position)
        {
            battleHUD = battleManager.playerHUD4.GetComponent<BattleHUD>();
        }
        else if (gameObject.transform.position == battleManager.playerTransform5.position)
        {
            battleHUD = battleManager.playerHUD5.GetComponent<BattleHUD>();
        }
        else if (gameObject.transform.position == battleManager.enemyTransform1.position)
        {
            battleHUD = battleManager.enemyHUD1.GetComponent<BattleHUD>();
        }
        else if (gameObject.transform.position == battleManager.enemyTransform2.position)
        {
            battleHUD = battleManager.enemyHUD2.GetComponent<BattleHUD>();
        }
        else if (gameObject.transform.position == battleManager.enemyTransform3.position)
        {
            battleHUD = battleManager.enemyHUD3.GetComponent<BattleHUD>();
        }
        else if (gameObject.transform.position == battleManager.enemyTransform4.position)
        {
            battleHUD = battleManager.enemyHUD4.GetComponent<BattleHUD>();
        }
        else if (gameObject.transform.position == battleManager.enemyTransform5.position)
        {
            battleHUD = battleManager.enemyHUD5.GetComponent<BattleHUD>();
        }

        maxHp = unitData.maxHp;
        maxMana = unitData.maxMana;
        unitName = unitData.name;

        unitElement = elementData.elementName;
        unitWeakToElement = elementData.weakAgainstElement;
        unitStrongAgainstElement = elementData.strongAgainstElement;
        unitWeakToElementModifier = elementData.weakToElementModifier;
        unitStrongAgainstModifier = elementData.strongAgainstElementModifier;
        elementSprite = elementData.elementSprite;
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
        {
            isUnitDead = true;
            return true;
        }
        else
        {
            isUnitDead = false;
            return false;
        }
    }
}
