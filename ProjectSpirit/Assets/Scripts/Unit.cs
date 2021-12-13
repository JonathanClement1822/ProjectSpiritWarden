using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{


    public string unitName;
    public int unitLevel;

    public int damage;

    //changed from int to float. Causes errors in BattleSystem.cs
    public int maxHP;
    //public int maxMP;
    //public int currentHP;

    public BattleHUD HUD;

    //public string HUDName;

    private void Awake()
    {
        //HUD = GameObject.Find(HUDName).GetComponent<BattleHUD>();
    }

    public bool TakeDamage(int dmg)
    {
        HUD.AdjustHealth(-dmg);

        if (HUD.healthValue <= 0)
            return true;
        else
            return false;

    }

    public void Heal(int amount)
    {
        HUD.AdjustHealth(amount);



    }
}
