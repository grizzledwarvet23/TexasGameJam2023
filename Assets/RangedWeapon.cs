using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, Weapon
{
    public bool automatic;
    public GameObject projectilePrefab;

    public Transform firePoint;

    public float fireDelay;

    public AudioSource shootSound;


    public void Attack() {
        if(automatic) {}
        else {
            Fire();
        }
    }

    void Fire() {
        //instantiate projectile prefab:
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        shootSound.Play();
    }

    public string GetType() {
        return "Ranged";
    }
}
