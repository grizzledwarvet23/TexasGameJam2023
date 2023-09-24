using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    //create a circle area around enemy that detects player
    [SerializeField]
    private float detectionRadius = 5f;

    //this is a prefab
    public GameObject damageMarker;

    private GameObject player;

    public float speed = 2f;

    private Rigidbody2D rb;

    public float trackDelay;

    private bool canTrack = true;

    private bool collidingWithPlayer = false;

    public AudioSource hitSound;

    bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        //player is find tag Player
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!dying) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
            bool foundPlayer = false;
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.tag == "Player") {
                    //move towards player
                    collider.gameObject.GetComponent<Player>().TakeDamage(1);
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
                rb.velocity = dir * speed;
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
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
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

