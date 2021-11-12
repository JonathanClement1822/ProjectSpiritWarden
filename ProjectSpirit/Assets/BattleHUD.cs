using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Image hpSlider;
    public string HUDName;

    private void Awake()
    {

        //gameObject.name = HUDName;

    }
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "LvL " + unit.unitLevel;
        //hpSlider.maxValue = unit.maxHP;
        hpSlider.fillAmount = unit.currentHP;


    }

    public void SetHP(int hp)
    {
        hpSlider.fillAmount = hp;
    }

}
