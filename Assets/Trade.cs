using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade : MonoBehaviour
{
    public int currentWeapon;
    public int nextWeapon;
    public int currentRarity;
    public int nextRarity;

    public int dayCount;

    // Start is called before the first frame update
    void Start()
    { 
        int RadNum = Random.Range(1,6);
        nextWeapon = RadNum;

        if (dayCount % 2 == 1){
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
