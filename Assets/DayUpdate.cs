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
            dayCount++;
            enemyHP *= 2;
            enemyDamage *= 1.5f;
            lie_prob += 10;
        }      
    }
}
