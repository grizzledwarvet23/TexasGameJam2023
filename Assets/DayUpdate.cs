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
        if (blackFade.GetCurrentAnimatorStateInfo(0).IsName("FadeOut"))
        {
            GameManager.instance.dayCount = dayCount + 1;
            GameManager.instance.enemyHP = enemyHP * 2;
            GameManager.instance.enemyDamage = enemyDamage * 1.5f;
            GameManager.instance.lie_prob = lie_prob + 10;
        }      
    }
}
