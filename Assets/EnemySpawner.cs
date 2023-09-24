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

    public Vector2[] topXSpawnPoints;
    public float[] topYSpawnPoint;

    public Vector2[] bottomXSpawnPoints;
    public float[] bottomYSpawnPoint;

    public Vector2[] rightYSpawnPoints;
    public float[] rightXSpawnPoint;

    public Vector2[] leftYSpawnPointsOne;
    public float[] leftXSpawnPointOne;

    public Vector2[] leftYSpawnPointsTwo;
    public float[] leftXSpawnPointTwo;
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
    }

    
}
