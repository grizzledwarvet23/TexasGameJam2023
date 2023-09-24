using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{

    public AudioSource firstMusic;
    public AudioSource secondMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.currentWave <= 5) {
            firstMusic.Play();
        } else {
            secondMusic.Play();
        }
    }

   
}
