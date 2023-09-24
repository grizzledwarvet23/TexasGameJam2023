using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemiesParent;

    private float spawnDelay;
    public static bool doneSpawning = false;

    public GameObject[] enemies;

    public GameObject transformsParent;
    private Transform currentTransform;

    public float waveDuration = 60; //60 seconds
    

    // Start is called before the first frame update
    void Start()
    {
        doneSpawning = false;
        if(GameManager.instance.currentWave <= 5) {
            spawnDelay = 1f;
        } else {
            spawnDelay = 0.5f;
        }
        //get child of transformsParent which is named the current wave
        currentTransform = transformsParent.transform.GetChild(GameManager.instance.currentWave - 1);
        StartCoroutine(SpawnEnemies());
        StartCoroutine(EndWave());

    }

    IEnumerator EndWave() {
        yield return new WaitForSeconds(waveDuration);
        //end wave
        doneSpawning = true;
    }


    IEnumerator SpawnEnemies() {
        yield return new WaitForSeconds(spawnDelay);
        //spawn enemy in the bounds; heres how it works:
        //transformParents children are labeled 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        //those children have children TL, TR, BL, BR representing the corners of the bounds
        //spawn the enemy  randomly at the bounds:
        if(GameManager.instance.currentWave == 1) {
            //choose random enemy from enemies array
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            //curretnTransform has four children: TL, TR, BL, BR
            //these mean top left, top right, bottom left, bottom right
            //spawn the enemies either between top left and top right, top left and bottom left, top right and bottom right, or bottom left and bottom right./
            //randomly choose the 4 sides then randomly choose a point on that side
            int side = Random.Range(0, 4);
            //top side
            if(side == 0) {
                //choose random x between TL and TR
                float x = Random.Range(currentTransform.GetChild(0).position.x, currentTransform.GetChild(1).position.x);
                //spawn enemy at (x, y) where y is the y of TL
                Instantiate(enemy, new Vector3(x, currentTransform.GetChild(0).position.y, 0), Quaternion.identity, enemiesParent.transform);
            } else if (side == 1) {
                //choose random x between BL and BR
                float x = Random.Range(currentTransform.GetChild(2).position.x, currentTransform.GetChild(3).position.x);
                //spawn enemy at (x, y) where y is the y of BL
                Instantiate(enemy, new Vector3(x, currentTransform.GetChild(2).position.y, 0), Quaternion.identity, enemiesParent.transform);
            } 
            // else if (side == 2) {
            //     //choose random y between TL and BL
            //     float y = Random.Range(currentTransform.GetChild(0).position.y, currentTransform.GetChild(2).position.y);
            //     //spawn enemy at (x, y) where x is the x of TL
            //     Instantiate(enemy, new Vector3(currentTransform.GetChild(0).position.x, y, 0), Quaternion.identity, enemiesParent.transform);
            // } 
            else {
                //choose random y between TR and BR
                float y = Random.Range(currentTransform.GetChild(1).position.y, currentTransform.GetChild(3).position.y);
                //spawn enemy at (x, y) where x is the x of TR
                Instantiate(enemy, new Vector3(currentTransform.GetChild(1).position.x, y, 0), Quaternion.identity, enemiesParent.transform);
            }
            
        }

    }

    
}
