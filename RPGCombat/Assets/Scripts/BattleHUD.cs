using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;

    public Slider hpSlider;
    public Slider manaSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;

        hpSlider.maxValue = unit.maxHp;
        hpSlider.value = unit.maxHp;

        manaSlider.maxValue = unit.maxMana;
        manaSlider.value = unit.maxMana;
    }

    public void SetHpAndMana(int hp, int mana)
    {
        hpSlider.value = hp;
        manaSlider.value = mana;
    }
}
