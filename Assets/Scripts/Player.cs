using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float xSpeed;
    public float ySpeed;

    public MeleeWeapon weapon;
    public GameObject weaponParent;
    // Start is called before the first frame update
    void Start()
    {
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
        if(!weapon.isAttacking) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = mousePos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weaponParent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(x * xSpeed, y * ySpeed);
    }
    
}
