using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use scene management to load scenes
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public Transform enemiesParent;
    bool activated = false;

    public Vector2 overlapBoxPosition;
    public Vector2 overlapBoxSize;

    bool doingShop = false;

    public Animator blackFade;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!activated && enemiesParent.childCount == 0) {
            activated = true;
        }
        
        if(!doingShop && activated) {
            //create a rectangular detection area around shop that detects player
            Collider2D[] colliders = Physics2D.OverlapBoxAll(overlapBoxPosition, overlapBoxSize, 0);
            bool foundPlayer = false;
            //move towards player
            foundPlayer = true;
            doingShop = true;
            StartCoroutine(GoToShop());
            
        }
    }

    IEnumerator GoToShop() {
        //do transition, go to shop scene, etc.
        GameManager.instance.currentWave = Mathf.Min(10, GameManager.instance.currentWave + 1);
        yield return new WaitForSeconds(1f);
        blackFade.Play("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Shop");
        
        


           
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(overlapBoxPosition, overlapBoxSize);
    }


}
