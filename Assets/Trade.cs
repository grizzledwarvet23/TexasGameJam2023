using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade : MonoBehaviour
{
    //GameManager variables
    public int dayCount;
    public int currentWeapon;
    public int nextWeapon;
    public int currentRarity;
    public int nextRarity;

    public int lie_prob;
    public int charDamage;

    // Start is called before the first frame update
    void Start()
    { 
        dayCount = GameManager.instance.dayCount;
        currentWeapon = GameManager.instance.currentWeapon;
        //nextWeapon = GameManager.instance.nextWeapon;
        currentRarity = GameManager.instance.currentRarity;
        nextRarity = GameManager.instance.nextRarity;
        lie_prob = GameManager.instance.lie_prob;
        charDamage = GameManager.instance.charDamage;

        int WeaponRadNum = Random.Range(1,6);
        nextWeapon = WeaponRadNum;

        int lieRadNum = Random.Range(0,100);

        if (lieRadNum < lie_prob){

            if (dayCount % 2 == 1 || currentWeapon == nextWeapon){
                nextRarity++;
            }
            GameManager.instance.currentWeapon = nextWeapon;
            if (currentRarity < nextRarity)
            {
                GameManager.instance.currentRarity = nextRarity;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
