using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemiesParent;

    private float spawnDelay;
    public static bool doneSpawning = false;

    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject enemyThree;
    public GameObject enemyFour;

    public GameObject transformsParent;

    // Start is called before the first frame update
    void Start()
    {
        doneSpawning = false;
        if(GameManager.instance.currentWave <= 5) {
            spawnDelay = 1f;
        } else {
            spawnDelay = 0.5f;
        }
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        yield return new WaitForSeconds(spawnDelay);
        //spawn enemy in the bounds; heres how it works:
        //transformParents children are labeled 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        //those children have children TL, TR, BL, BR representing the corners of the bounds
        //spawn the enemy  randomly at the bounds:
        if(GameManager.instance.currentWave == 1) {
            //random number from 1 to 4
            int enemyNum = Random.Range(1, 5);
            GameObject enemy;
            if(enemyNum == 1) {
                enemy = Instantiate(enemyOne, transformsParent.transform.GetChild(0).GetChild(0).position, Quaternion.identity);
            } else if(enemyNum == 2) {
                enemy = Instantiate(enemyTwo, transformsParent.transform.GetChild(0).GetChild(1).position, Quaternion.identity);
            } else if(enemyNum == 3) {
                enemy = Instantiate(enemyThree, transformsParent.transform.GetChild(0).GetChild(2).position, Quaternion.identity);
            } else {
                enemy = Instantiate(enemyFour, transformsParent.transform.GetChild(0).GetChild(3).position, Quaternion.identity);
            }
        }

    }

    
}
