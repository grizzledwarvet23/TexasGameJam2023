using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{

    public AudioSource firstMusic;
    public AudioSource secondMusic;

    public AudioSource thirdMusicIntro;
    public AudioSource thirdMusicLoop;
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.currentWave <= 3) {
            firstMusic.Play();
        } else if (GameManager.instance.currentWave <= 7) {
            secondMusic.Play();
        } else {
            StartCoroutine(PlayThirdMusic());
        }
    }

    //ienumerator PlayThirdMusic that plays intro and plays the loop after the intro
    IEnumerator PlayThirdMusic() {
        thirdMusicIntro.Play();
        yield return new WaitForSeconds(thirdMusicIntro.clip.length);
        thirdMusicLoop.Play();
    }

   
}
