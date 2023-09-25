using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int baseLineHealth = 3;

    private int health;

    //create a circle area around enemy that detects player
    [SerializeField]
    private float detectionWidth = 5f;
    [SerializeField]
    private float detectionHeight = 5f;
    //private float detectionRadius = 5f;

    //this is a prefab
    public GameObject damageMarker;

    private GameObject player;

    public float baseSpeed = 2f;

    public int baseAttack = 2;

    private int attack;

    private float speed = 2f;


    private Rigidbody2D rb;

    public float trackDelay;

    private bool canTrack = true;

    private bool collidingWithPlayer = false;

    public AudioSource hitSound;

    bool dying = false;

    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        int wave = GameManager.instance.currentWave;
        //health is 2 to the power of (wave - 1), times baseline health\
        health = (int) Mathf.Pow(1.15f, wave - 1) * baseLineHealth;
        //print health
        Debug.Log("enemy health: " + health);
        speed = baseSpeed + (wave - 1) * 0.15f;


        //attack should be 1.5 the attack of the previous wave
        attack = (int) Mathf.Pow(1.2f, wave - 1) * baseAttack;
        Debug.Log("Enemy attack: " + attack);

        //player is find tag Player
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        //get this objects parent
        parent = transform.parent.gameObject;

    }

    void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(!dying) {

            if(player != null) {
            if(player.transform.position.x < transform.position.x) {
                //player is to the left of enemy
                if(transform.localScale.x > 0) {
                    Flip();
                }
            } else {
                //player is to the right of enemy
                if(transform.localScale.x < 0) {
                    Flip();
                }
            }
        }
            // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
            // bool foundPlayer = false;
            // foreach (Collider2D collider in colliders) {
            //     if (collider.gameObject.tag == "Player") {
            //         //move towards player
            //         collider.gameObject.GetComponent<Player>().TakeDamage(1);
            //         foundPlayer = true;
            //     }
            // }
            //instgead of using overlap circle, lets do a box:
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(detectionWidth, detectionHeight ), 0);
            bool foundPlayer = false;
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.tag == "Player") {
                    //move towards player
                    collider.gameObject.GetComponent<Player>().TakeDamage(attack);
                    foundPlayer = true;
                }
            }

            if(foundPlayer)
            {
                collidingWithPlayer = true;
            }
            else
            {
                collidingWithPlayer = false;
            }
        }
        
    }

    IEnumerator SetCanTrack(float delay) {
        canTrack = false;
        yield return new WaitForSeconds(delay);
        canTrack = true;
    }

    void FixedUpdate() {
        if(dying) {
            rb.velocity = Vector2.zero;
        } else {
            //get direction to player, normalize it, and multiply by speed
            if(collidingWithPlayer) {
                rb.velocity = Vector2.zero;
            }
            else if(player != null && canTrack) {
                Vector2 dir = player.transform.position - transform.position;
                dir.Normalize();
                Vector2 motionVector = dir * speed;

                //iterate through enemies, the child of the parent. find the closest one that is not this one. to the motion vector, add direction away from that enemy
                //this will make it so that enemies dont bump into each other
                float minDistance = Mathf.Infinity;
                GameObject closestEnemy = null;
                foreach(Transform child in parent.transform) {
                    if(child.gameObject != gameObject) {
                        float distance = Vector2.Distance(child.position, transform.position);
                        if(distance < minDistance) {
                            minDistance = distance;
                            closestEnemy = child.gameObject;
                        }
                    }
                }
                if(closestEnemy != null) {
                    Vector2 awayFromEnemy = transform.position - closestEnemy.transform.position;
                    awayFromEnemy.Normalize();
                    motionVector += (0.5f) * speed * awayFromEnemy;
                }
                rb.velocity = motionVector;


                //also consider we dont want to bump into other enemies.
                //so we need to check if there are any enemies in the direction we are going
                //the parent has a list of all enemies. we can check if any of them are in the direction we are going
                //if they are, we can set the direction to be perpendicular to the direction we are going:
                StartCoroutine(SetCanTrack(trackDelay)); 

            }
        }

    }

    //draw it
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(detectionWidth, detectionHeight, 0));
    }
    public void TakeDamage(int deltaHealth) {
        //instantiate damage marker in a random position near enemy. it has a child canvas, which has a child Count which is
        //textmeshpro text. set the text to deltaHealth
        GameObject marker = Instantiate(damageMarker, transform.position + new Vector3(Random.Range(-2f, -0.5f), Random.Range(0f, 1f), 0), Quaternion.identity);
        marker.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = deltaHealth.ToString();
        hitSound.Play();
        health -= deltaHealth;
        if (health <= 0) {
            //set sprite renderer and collider to false
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            dying = true;
            StartCoroutine(DestroySelf(1f));
        }
    }

    IEnumerator DestroySelf(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

