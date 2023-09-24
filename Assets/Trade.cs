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
