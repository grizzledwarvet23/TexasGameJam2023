using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public AudioSource pauseMusic;


    //create dynamic arraylsit of playing audio sources
    List<AudioSource> playingSources = new List<AudioSource>();



    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("YES");
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        //continue all the ones in the arraylist
        foreach (var source in playingSources)
        {
            source.UnPause();
        }
        if(pauseMusic != null) {
            pauseMusic.Stop();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;


        //clear out the arraylist
        playingSources.Clear();
        foreach (var source in FindObjectsOfType<AudioSource>())
        {
            //for all the ones that are playing, add them to the arraylist
            if (source.isPlaying)
            {
                playingSources.Add(source);
                source.Pause();
                Debug.Log(source.clip.name);
            }
        }
        if(pauseMusic != null) {
            pauseMusic.Play();
        }

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
