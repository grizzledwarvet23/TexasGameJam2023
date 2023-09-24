using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemiesParent;
    public static bool doneSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        doneSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
