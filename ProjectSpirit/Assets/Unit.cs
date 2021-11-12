using UnityEngine;

public class Unit : MonoBehaviour
{


    public string unitName;
    public int unitLevel;

    public int damage;

    //changed from int to float. Causes errors in BattleSystem.cs
    public int maxHP;
    public int currentHP;

    public BattleHUD HUD;

    public string HUDName;

    private void Awake()
    {
        //HUD = GameObject.Find(HUDName).GetComponent<BattleHUD>();
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;

    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}
