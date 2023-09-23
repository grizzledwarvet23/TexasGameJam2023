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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject.tag == "Player") {
                //move towards player
                collider.gameObject.GetComponent<Player>().TakeDamage(1);
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
        
        health -= deltaHealth;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}

