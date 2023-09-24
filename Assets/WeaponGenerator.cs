using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponGenerator : MonoBehaviour
{

    public GameObject[] GoodWeapons;
    public GameObject[] badWeapons;

    public string[] weaponNames;

    [System.NonSerialized]
    public string nameToChoose;

    [System.NonSerialized]
    public GameObject weaponToChoose;

    public Image singleWeaponImage;
    public TextMeshProUGUI singleWeaponText;

    public Image weaponOneImage;

    public Image weaponTwoImage;

    [System.NonSerialized]
    public float lieProbability;



    public Sprite swordImage;

    public Sprite spearImage;

    public Sprite bowImage;
    public Sprite gunImage;

    [System.NonSerialized]
    public bool badChoice; 

    [System.NonSerialized]
    public GameObject weaponParent;
    // Start is called before the first frame update
    void Start()
    {
        badChoice = false;
        GameManager gm = GameManager.instance;

        //base on wave number (from 1 to 10), lieProbability is lerp from 30 to 80
        lieProbability = Mathf.Lerp(0.3f, 0.8f, gm.currentWave / 10f);

        Debug.Log(lieProbability);



        // GameObject weapon = null;GameObject weapon = null;
        if(gm.currentWave <= 10) {
             //else {
                //choose bad weapon based on lie probability. so if lie probability is 30, then 30% chance of getting a bad weapon
                //generate float between 0 and 1
                float rand = Random.Range(0f, 1f);
                Debug.Log(rand);
                if(rand < lieProbability) {
                    //choose bad weapon
                    int rand2 = Random.Range(0, badWeapons.Length);
                    weaponParent = badWeapons[rand2];
                    badChoice = true;
                } else {
                    //choose good weapon
                    int rand2 = Random.Range(0, GoodWeapons.Length);
                    weaponParent = GoodWeapons[rand2]; //use type for the thing
                    badChoice = false;
                }
                
            //}
            nameToChoose = weaponNames[Random.Range(0, weaponNames.Length)];
            //Weapon weaponScript is weapon's child, which has Weapon script
            Weapon weaponScript = weaponParent.GetComponentInChildren<Weapon>();

            switch (weaponScript.GetType()) {
                case "Sword":
                    singleWeaponImage.sprite = swordImage;
                    // weaponOneImage.sprite = swordImage;
                    // weaponTwoImage.sprite = swordImage;
                    break;
                case "Spear":
                    singleWeaponImage.sprite = spearImage;
                    // weaponOneImage.sprite = spearImage;
                    // weaponTwoImage.sprite = spearImage;
                    break;
                case "Bow":
                    singleWeaponImage.sprite = bowImage;
                    // weaponOneImage.sprite = bowImage;
                    // weaponTwoImage.sprite = bowImage;
                    break;
                case "Gun":
                    singleWeaponImage.sprite = gunImage;
                    // weaponOneImage.sprite = gunImage;
                    // weaponTwoImage.sprite = gunImage;
                    break;
            }

            



            
        }









    }

    
}
