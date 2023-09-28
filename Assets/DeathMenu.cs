using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    //public static DeathMenu instance;
    public static bool GameOver = false;
    public GameObject deathMenuUI;




    public GameObject pause;
    public GameObject regular;
    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        regular.SetActive(false);
    }




    public void Death()
    {
        Debug.Log("HEY");
        GameManager.instance.currentWave = 1;
        GameManager.instance.currentWeapon = null;
        deathMenuUI.SetActive(true);
        GameOver = true;
        //set this object to active:
        this.gameObject.SetActive(true);
//        Time.timeScale = 0f;
    }

    public void Restart()
    {
        deathMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        GameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
