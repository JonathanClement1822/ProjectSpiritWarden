using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBars : MonoBehaviour
{
    public Image hpBar;
    public Image mpBar;

    public int healthValue;
    public int manaValue;

    public int maxHP = 75;
    public int maxMP = 23;

    private void Start()
    {
        healthValue = maxHP;
        manaValue = maxMP;

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

    private void Update()
    {
        float hpFloat = healthValue;
        float mpFloat = manaValue;

        hpBar.fillAmount = UtilScript.RemapRange(hpFloat, 0, maxHP, 0, 1);
        mpBar.fillAmount = UtilScript.RemapRange(mpFloat, 0, maxMP, 0, 1);

        /*if (Input.GetKeyDown(KeyCode.A))
        {
            int num = Random.Range(1, (maxHP / 4));
            AdjustHealth(-num);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            int num = Random.Range(1, (maxHP / 4));
            AdjustHealth(num);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            int num = Random.Range(1, (maxMP / 4));
            AdjustMana(num);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            int num = Random.Range(1, (maxMP / 4));
            AdjustMana(-num);
        }*/
    }
}
