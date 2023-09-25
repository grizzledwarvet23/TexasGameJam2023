using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;

    public string positiveResponse;
    public string negativeResponse;

    private bool givingFinalResponse = false;
    private bool goodResponse = false;

    private bool isTransitioning = false;

    public bool[] stopPoints; //corresponds to lines
    public float textSpeed;

    private int index;

    public AudioSource blipSound;


    public Animator blackFade;

    public GameObject playButton, exitButton;

    public Image merchantImage;
    public Sprite[] merchantSprites;

    public GameObject weaponChoice;

    public Image weaponImage;
    public TextMeshProUGUI weaponName;

    public WeaponGenerator weaponGenerator;



    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        blackFade.Play("FadeIn");

        
        //invoke start dialogue after 1 second
        Invoke("StartDialogue", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!givingFinalResponse) {
                if (textComponent.text != lines[index])
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                    if(!stopPoints[index]) {
                        StartCoroutine(DoNextLine(1f));
                    } else {
                        if(!playButton.activeSelf) {
                            playButton.SetActive(true);
                            exitButton.SetActive(true);
                            merchantImage.sprite = merchantSprites[1];
                            weaponChoice.SetActive(true);



                        }
                    }
                }
            } else {
                if(goodResponse) {
                    if(textComponent.text != positiveResponse) {
                        StopAllCoroutines();
                        textComponent.text = positiveResponse;
                        StartCoroutine(EndDialogue());
                    }
                }
                else {
                    if(textComponent.text != negativeResponse) {
                        StopAllCoroutines();
                        textComponent.text = negativeResponse;
                        StartCoroutine(EndDialogue());
                    }
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0; 
        StartCoroutine(TypeLine());
    }

    IEnumerator EndDialogue() 
    {
        if(!isTransitioning) 
        {
            isTransitioning = true;
            yield return new WaitForSeconds(1f);
            blackFade.Play("FadeOut");
            yield return new WaitForSeconds(1f);
            LoadGameScene();
        }
        
    }

    IEnumerator TypeLine()
    {
        //type each character
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            blipSound.Play();
            yield return new WaitForSeconds(textSpeed);
        }
        if(!stopPoints[index]) {
            StartCoroutine(DoNextLine(1f));
        }
        else if(!playButton.activeSelf) {
            playButton.SetActive(true);
            exitButton.SetActive(true);
        }
        //StartCoroutine(DoNextLine(1));
    }

    IEnumerator TypeLine(string line, bool isLastLine)
    {
        textComponent.text = string.Empty;
        //type each character
        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c;
            blipSound.Play();
            yield return new WaitForSeconds(textSpeed);
        }

        if(isLastLine) {
            StartCoroutine(EndDialogue());
        }

        
   

    }

    IEnumerator DoNextLine(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextLine();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        // else
        // {
        //     gameObject.SetActive(false);
        //     Invoke("LoadGameScene", 1f);

        // }
    }

    public void HandlePlayerDecision(bool accepted)
    {
        merchantImage.sprite = merchantSprites[0];
        weaponChoice.SetActive(false);
        givingFinalResponse = true;
        if(accepted)
        {
            goodResponse = true;
            StartCoroutine(TypeLine(positiveResponse, true));
        } else {
            goodResponse = false;
            StartCoroutine(TypeLine(negativeResponse, true));
        }

        //turn off buttons
        playButton.SetActive(false);
        exitButton.SetActive(false);
    }

    void LoadGameScene() {
        SceneManager.LoadScene("Game");
    }
}
