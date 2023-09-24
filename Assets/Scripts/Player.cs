using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float xSpeed;
    public float ySpeed;

    public Weapon weapon;
    public GameObject weaponParent;

    public Slider healthBar;
    public int health = 10;
    private int healthMax;

    private bool invincible = false;
    // Start is called before the first frame update
    void Start()
    {
        //weapon s get child of weapon parent
        weapon = weaponParent.GetComponentInChildren<Weapon>();
        invincible = false;
        healthMax = health;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //rotate weapon according to mouse position. dont make it too sensitive when mouse is near player
        RotateWeapon();


        //if user clicks left click, get component 
        if (Input.GetMouseButtonDown(0)) {
            weapon.Attack();
        }
    }

    void RotateWeapon() {
        if(weapon.GetType() == "Melee") {
            //cast weapon to MeleeWeapon
            MeleeWeapon meleeWeapon = (MeleeWeapon)weapon;
            if(!meleeWeapon.isAttacking) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 dir = mousePos - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                weaponParent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        } else {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = mousePos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weaponParent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void TakeDamage(int deltaHealth) {
        if(!invincible) {
            StartCoroutine(DoInvincibility(1f));
            health -= deltaHealth;
            healthBar.value = health / (float)healthMax;
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DoInvincibility(float delay) {
        invincible = true;
        yield return new WaitForSeconds(delay);
        invincible = false;
    }

    void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(x * xSpeed, y * ySpeed);
    }
    
}
