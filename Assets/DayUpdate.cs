using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayUpdate : MonoBehaviour
{
    public int dayCount;
    public int enemyHP;
    public float enemyDamage;
    public int lie_prob;
    public Animator blackFade;

    void Start()
    {
            GameManager.instance.dayCount += 1;
            GameManager.instance.enemyHP *= 2;
            GameManager.instance.enemyDamage *= 1.5f;
            GameManager.instance.lie_prob += 10;
    }
}
