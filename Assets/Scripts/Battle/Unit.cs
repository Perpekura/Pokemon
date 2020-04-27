using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLev;

    public int damage;

    public int maxHP;
    public int currentHP;

    public bool isPlayer;

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;

        }
          
        else
            return false;
    }
    
}
