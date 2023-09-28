using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    Rigidbody2D rb;
    public float xSpeed;
    public float ySpeed;

    public Weapon weapon;
    public GameObject weaponParent;

    public Slider healthBar;
    public int health = 10;
    private int healthMax;

    private bool invincible = false;

    public DeathMenu deathMenu;

    private Animator animator;

    public GameObject stick;
    // Start is called before the first frame update
    void Start()
    {

        // if(GameManager.instance.currentWeapon == null && weaponParent == null) {
        //     Debug.Log(GameManager.instance.currentWeapon);
        //     weaponParent = stick;
        //     weaponParent = Instantiate(weaponParent, transform.position, Quaternion.identity); 
        //     weaponParent.transform.parent = transform;
        //     weapon = weaponParent.GetComponentInChildren<Weapon>();
        // } else {
        if(GameManager.instance.justTraded && GameManager.instance.currentWeapon != null) { //traded for new weapon
                Debug.Log("TWO");
                //Destroy(weaponParent);
                weaponParent = GameManager.instance.currentWeapon;
                //instantiate weapon parent as a child of this player
                weaponParent = Instantiate(weaponParent, transform.position, Quaternion.identity);
                weaponParent.transform.parent = transform;
                // if(GameManager.instance.gotBadWeapon) {
                weapon = weaponParent.GetComponentInChildren<Weapon>();

                weapon.SetDamage(GameManager.instance.damageToDo);

        }
        else if(GameManager.instance.currentWeapon != null) { //keeping same one
            Debug.Log("THREE");
            Destroy(weaponParent);
            weaponParent = GameManager.instance.currentWeapon;
            //instantiate weapon parent as a child of this player
            weaponParent = Instantiate(weaponParent, transform.position, Quaternion.identity);
            weaponParent.transform.parent = transform;
            // if(GameManager.instance.gotBadWeapon) {
            weapon = weaponParent.GetComponentInChildren<Weapon>();

            weapon.SetDamage(GameManager.instance.damageToDo);
        }
        else if(weaponParent == null) { //starting out
            Debug.Log("ONE");
            weaponParent = Instantiate(stick, transform.position, Quaternion.identity);
            weaponParent.transform.parent = transform;
            weapon = weaponParent.GetComponentInChildren<Weapon>();
        }
         else {
            Debug.Log("FOUR");
            weapon = weaponParent.GetComponentInChildren<Weapon>();

        }
        
        invincible = false;
        healthMax = health;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
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
        if(weapon.GetType() == "Sword" || weapon.GetType() == "Spear" || weapon.GetType() == "Stick" ) {
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
            StartCoroutine(DoFlashing(1f));
            health -= deltaHealth;
            healthBar.value = health / (float)healthMax;
            if (health <= 0) {
                deathMenu.Death();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DoFlashing(float delay) {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        for(int i = 0; i < 5; i++) {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(delay/10);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(delay/10);
        }
    }

    IEnumerator DoInvincibility(float delay) {
        invincible = true;
        //set layer to the one called "Invulnerable"
        gameObject.layer = 8;
        yield return new WaitForSeconds(delay);
        gameObject.layer = 0;
        invincible = false;
    }

    void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(x * xSpeed, y * ySpeed);

        animator.SetInteger("x_dir", (int)x);
        animator.SetInteger("y_dir", (int)y);
    }
    
}
