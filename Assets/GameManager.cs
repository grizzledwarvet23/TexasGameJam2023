using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//get cinemachine
using Cinemachine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    
    public GameObject[] waveTiles;
    public int currentWave = 1;

    public GameObject[] weapon_types;
    public GameObject currentWeapon;

    public int currentRarity = 1;

    public int nextRarity = 1;

    public int dayCount = 1;
    public int enemyHP = 5;
    public float enemyDamage = 1;

    public int charHP = 100;
    public int charDamage = 1;

    public int lie_prob = 0;

    public float widestCamera;
    
    public float narrowestCamera;

    [System.NonSerialized]
    public bool justTraded = false;

    [System.NonSerialized]
    public bool gotBadWeapon;

    [System.NonSerialized]
    public int damageToDo = 1;


    //cinemachine virtual camera
    public CinemachineVirtualCamera vcam;

    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
    }

    void OnEnable() {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if(scene.name == "Game") {
            //in the scene hierarchy, the gameobject "Grid" has child "Walls." waveTiles are assigned to the children of walls
            //loop through and assign waveTiles to the children of walls
            GameObject walls = GameObject.Find("Walls");
            if(walls != null) {
                for(int i = 0; i < walls.transform.childCount; i++) {
                    waveTiles[i] = walls.transform.GetChild(i).gameObject;
                }
                

                for(int i = 0; i < waveTiles.Length; i++) 
                {
                    waveTiles[i].SetActive(false);
                }
                //remaining set to false:
                waveTiles[currentWave - 1].SetActive(true);
            }
            // if(currentWeapon < weapon_types.Length) {
            //     weapon_types[currentWeapon - 1].SetActive(true);
            // }
            vcam = FindObjectOfType<CinemachineVirtualCamera>();
            float lerpValue = (float)currentWave / (float)waveTiles.Length;
            float cameraSize = Mathf.Lerp(widestCamera, narrowestCamera, lerpValue);
            vcam.m_Lens.OrthographicSize = cameraSize;
            Debug.Log("Yo " + cameraSize);
        }
        //if scene name is "Menu", just 0 out all the variables
        if(scene.name == "Menu") {
            currentWave = 1;
            currentRarity = 1;
            nextRarity = 1;
            dayCount = 1;
            enemyHP = 5;
            enemyDamage = 1;
            charHP = 100;
            charDamage = 1;
            lie_prob = 0;
            justTraded = false;
            gotBadWeapon = false;
            damageToDo = 1;
        }


    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        if(vcam == null) 
        {
            vcam = FindObjectOfType<CinemachineVirtualCamera>();
        }
        if(waveTiles[0] != null) {
            for(int i = 0; i < waveTiles.Length; i++) 
            {
                waveTiles[i].SetActive(false);
            }
            
            //remaining set to false:
            waveTiles[currentWave - 1].SetActive(true);
        }
        if(vcam != null) {
            //based on wave, lerp from widest to narrowest
            float lerpValue = (float)currentWave / (float)waveTiles.Length;
            float cameraSize = Mathf.Lerp(widestCamera, narrowestCamera, lerpValue);
            vcam.m_Lens.OrthographicSize = cameraSize;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
