using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject[] menuObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackFromCredits() {
        //set all menu objects to active:
        foreach (GameObject obj in menuObjects) {
            obj.SetActive(true);
        }
        //set parent object to inactive:
        this.transform.parent.gameObject.SetActive(false);
    }
}
