using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rb.velocity = transform.right * speed;
    }
    

    //ontrigger enter, if tag is Enemy, call enemy's damage method. Destroy bullet
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null) {
                enemy.TakeDamage(1);
            }
        }
        Destroy(gameObject);
    }
}
