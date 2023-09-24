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
        int RadNum = Random.Range(1,6);
        nextWeapon = RadNum;

        //if (lie_prob)

        if (dayCount % 2 == 1 || currentWeapon == nextWeapon){
            nextRarity++;
        }
        currentWeapon = nextWeapon;
        if (currentRarity < nextRarity)
        {
            currentRarity = nextRarity;
        }
    }

}
