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

    // Start is called before the first frame update
    void Start()
    { 
        int RadNum = Random.Range(1,6);
        nextWeapon = RadNum;

        if (dayCount % 2 == 1 || currentWeapon == nextWeapon){
            nextRarity++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = nextWeapon;
        dayCount++;
        if (currentRarity < nextRarity)
        {
            currentRarity = nextRarity;
        }
    }
}
