using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade : MonoBehaviour
{
    //GameManager variables
    public int dayCount;
    public int currentWeapon;
    public int currentRarity;
    public int nextRarity;

    public int lie_prob;
    public int charDamage;

    public WeaponGenerator weaponGenerator;

    // Start is called before the first frame update
    void Start()
    { 
        // dayCount = GameManager.instance.dayCount;
        // currentWeapon = GameManager.instance.currentWeapon;
        // //nextWeapon = GameManager.instance.nextWeapon;
        // currentRarity = GameManager.instance.currentRarity;
        // nextRarity = GameManager.instance.nextRarity;
        // lie_prob = GameManager.instance.lie_prob;
        // charDamage = GameManager.instance.charDamage;

        // int WeaponRadNum = Random.Range(1,6);
        // nextWeapon = WeaponRadNum;

        // int lieRadNum = Random.Range(0,100);

        // if (lieRadNum < lie_prob){

        //     if (dayCount % 2 == 1 || currentWeapon == nextWeapon){
        //         nextRarity++;
        //     }
        //     GameManager.instance.currentWeapon = nextWeapon;
        //     if (currentRarity < nextRarity)
        //     {
        //         GameManager.instance.currentRarity = nextRarity;
        //     }
        // }
    }

    public void TradeItem() {
        int oldDamage = GameManager.instance.damageToDo;
        GameManager.instance.currentWeapon = weaponGenerator.weaponParent;
        GameManager.instance.justTraded = true;
        if(weaponGenerator.badChoice) {
            GameManager.instance.gotBadWeapon = true;
            GameManager.instance.damageToDo =  Mathf.Max(1, (int) 0.75f * oldDamage);

        } else {
            GameManager.instance.gotBadWeapon = false;
            GameManager.instance.damageToDo = 2* oldDamage;
        }
    }

    public void AntiTrade() {
        GameManager.instance.justTraded = false;
        GameManager.instance.gotBadWeapon = false;
    }

}
