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
        
        
        if(!doingShop && enemiesParent.childCount == 0 && EnemySpawner.doneSpawning ) {
            //create a rectangular detection area around shop that detects player
            
            doingShop = true;
            StartCoroutine(GoToShop());
            
        }
    }

    IEnumerator GoToShop() {
        //do transition, go to shop scene, etc.
        GameManager.instance.currentWave = Mathf.Min(11, GameManager.instance.currentWave + 1);
        yield return new WaitForSeconds(1f);
        blackFade.Play("FadeOut");
        yield return new WaitForSeconds(1f);
        if(GameManager.instance.currentWave >= 11) {
            SceneManager.LoadScene("MainMenu");
        } else {
            SceneManager.LoadScene("Shop");
        }
        
        
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(overlapBoxPosition, overlapBoxSize);
    }


}
