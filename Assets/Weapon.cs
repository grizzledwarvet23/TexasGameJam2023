using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon
{
    public void SetDamage(int damage);
    public void Attack();

    public string GetType();
}