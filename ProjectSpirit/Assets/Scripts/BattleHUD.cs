using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public Image hpBar;
    public Image mpBar;

    public int healthValue;
    public int manaValue;

    public int maxHP = 75;
    public int maxMP = 23;

    public TMP_Text nameText;
    public TMP_Text levelText;

    //public Image hpSlider;
    //public string HUDName;

    private void Start()
    {

        //gameObject.name = HUDName;

    }

    private void Update()
    {
        float hpFloat = healthValue;
        float mpFloat = manaValue;

        hpBar.fillAmount = UtilScript.RemapRange(hpFloat, 0, maxHP, 0, 1);
        mpBar.fillAmount = UtilScript.RemapRange(mpFloat, 0, maxMP, 0, 1);
    }
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "LvL " + unit.unitLevel;
        healthValue = unit.maxHP;
        maxHP = unit.maxHP;
        //manaValue = unit.maxMP;



    }

    public void AdjustHealth(int value)
    {
        if (healthValue + value <= maxHP)
        {
            if (healthValue + value >= 0)
            {
                healthValue += value;
            }
            else
            {
                healthValue = 0;
            }

        } else
        {
            healthValue = maxHP;
        }

    }
    public void AdjustMana(int value)
    {
        if (manaValue + value <= maxMP)
        {
            if (manaValue + value >= 0)
            {
                manaValue += value;
            }
            else
            {
                manaValue = 0;
            }

        }
    }


}
