using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, Weapon
{
    [System.NonSerialized]
    public bool isAttacking = false;
    public float attackDuration = 0.25f;
    public AudioSource attackSound;

    private bool justAttacked = false;

    private BoxCollider2D boxCollider;

    public bool slasher = true;

    public string type;

    public int attackAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //on collision with enemy, call enemy's TakeDamage method
    void OnTriggerEnter2D(Collider2D other) {
        if (isAttacking && !justAttacked) {
            Enemy enemy = other.GetComponent<Enemy>();
            // Debug.Log("Enemy: " + enemy);
            if (enemy != null) {
                justAttacked = true;
                StartCoroutine(SetJustAttacked(false, 0.1f));
                enemy.TakeDamage(attackAmount);
            }
        }
    }

    IEnumerator SetJustAttacked(bool value, float delay) {
        yield return new WaitForSeconds(delay);
        justAttacked = value;
    }



    //create a coroutine that moves weapon forward for delay/2, then moves it back for delay/2:
    IEnumerator MoveWeaponForward(float delay) {
        for (int i = 0; i < 10; i++) {
            transform.Translate(Vector3.right * 0.1f);
            yield return new WaitForSeconds(delay/25);
        }
        for (int i = 0; i < 10; i++) {
            transform.Translate(Vector3.left * 0.1f);
            yield return new WaitForSeconds(delay/25);
        }
    }

    IEnumerator RotateSword(float delay) {
        //start sword a little back
        transform.parent.transform.Rotate(Vector3.forward * 45);
        for (int i = 0; i < 60; i++) {
            transform.parent.transform.Rotate(Vector3.back * (9 / 3.0f));

            yield return new WaitForSeconds(delay/60);
        }
        // for (int i = 0; i < 10; i++) {
        //     transform.parent.transform.Rotate(Vector3.back * 9);
        //     yield return new WaitForSeconds(delay/25);
        // }
        //reset sword to original position
        transform.parent.transform.Rotate(Vector3.forward * 45);
    }

    IEnumerator ThrustSword(float delay) {
        //instead of rotate like RotateSword, just move forward and back
        for (int i = 0; i < 10; i++) {
            transform.Translate(Vector3.right * 0.1f);
            yield return new WaitForSeconds(delay/25);
        }
        for (int i = 0; i < 10; i++) {
            transform.Translate(Vector3.left * 0.1f);
            yield return new WaitForSeconds(delay/25);
        }
    }

    public string GetType() {
        return type;
    }

    public void SetDamage(int damage) {
        attackAmount = damage;
    }




    public void Attack() {
        if(!isAttacking) {
            isAttacking = true;
            boxCollider.enabled = true;
            attackSound.Play();
            StartCoroutine(SetAttacking(false, attackDuration));
            //StartCoroutine(MoveWeaponForward(attackDuration));
            if(slasher) {
                StartCoroutine(RotateSword(attackDuration));
            } else {
                StartCoroutine(ThrustSword(attackDuration));
            }
        }
    }

    IEnumerator SetAttacking(bool value, float delay) {
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = false;
        isAttacking = value;
    }
}
