using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [System.NonSerialized]
    public bool isAttacking = false;
    public float attackDuration = 0.25f;

    private BoxCollider2D boxCollider;
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
        if (isAttacking) {
            Enemy enemy = other.GetComponent<Enemy>();
            Debug.Log("Enemy: " + enemy);
            if (enemy != null) {

                enemy.TakeDamage(1);
            }
        }
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

    public void Attack() {
        isAttacking = true;
        boxCollider.enabled = true;
        StartCoroutine(SetAttacking(false, attackDuration));
        StartCoroutine(MoveWeaponForward(attackDuration));
    }

    IEnumerator SetAttacking(bool value, float delay) {
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = false;
        isAttacking = value;
    }
}
