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

    public float widestCamera;
    public float narrowestCamera;

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
       
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        if(vcam == null) 
        {
            vcam = FindObjectOfType<CinemachineVirtualCamera>();
        }
        for(int i = 0; i < waveTiles.Length; i++) 
        {
            waveTiles[i].SetActive(false);
        }
        //remaining set to false:
        waveTiles[currentWave - 1].SetActive(true);

        //based on wave, lerp from widest to narrowest
        float lerpValue = (float)currentWave / (float)waveTiles.Length;
        float cameraSize = Mathf.Lerp(widestCamera, narrowestCamera, lerpValue);
        vcam.m_Lens.OrthographicSize = cameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
