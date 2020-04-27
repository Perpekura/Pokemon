using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;

    public Slider hpSlider;

    public Text maxHP;
    public Text currentHP;


    public void setHUD(Unit unit) {

        nameText.text = unit.unitName;
        levelText.text = "Lv:"+unit.unitLev;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

        if (unit.isPlayer) {
            maxHP.text = unit.maxHP.ToString();
            currentHP.text = unit.currentHP.ToString();
        }
       

        
    }

    public void setHP(int hp) {

        hpSlider.value = hp;
    }
    
}
