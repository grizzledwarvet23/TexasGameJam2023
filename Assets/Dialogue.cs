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
        lines = new string[3];
        ChangeDialogue();
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

    public void ChangeDialogue(){
        switch (GameManager.instance.dayCount)
        {
            case 1:
                lines[0] = "Is that a visitor I see? Haven't seen one in years.. Welcome to my shack.";
                lines[1] = "...Dangerous monsters you say? Yeah, you can't survive with that stick of yours. Heh.";
                lines[2] = "Tell you what. I want to leave this place as much as you do. If you defeat all the monsters, I'll give you \"STRONGER\" weapons, starting with this one. Care to lend me a hand?";
                positiveResponse = "Hehehehehe. I knew you were the one.. Good luck out there.";
                negativeResponse = "Hehehehehe. You'll need me, sooner or later... It's only a matter of time.";
                break;
            case 2:
                lines[0] = "Hehe. Did you enjoy your decision? Like it or not, I'm pretty generous to give you more offers.";
                lines[1] = "You probably realized that you don't have much of a choice but to stay inside during the night, ay?";
                lines[2] = "Well then, here's the second weapon I offer. Make your choice..";
                positiveResponse = "Hehehehehe. Lovely... See you tomorrow.";
                negativeResponse = "Hmm? Don't trust yourself too much.. In the end, you'll see who's right.. Hehe...";
                break;
            case 3:
                lines[0] = "Did you notice enemies getting stronger? Harder to kill, easier to die...";
                lines[1] = "You see why you need to trade with me? I am your SAVIOR. Your ONE WAY PATH...";
                lines[2] = "Besides...what is there for you to lose?";
                positiveResponse = "Hehehehe. Delicious.";
                negativeResponse = "Wrong answer. HEHEHEHEHEHEHE";
                break;
            case 4:
                lines[0] = "What's that? Trees moving closer? Have you lost your mind? Of course you have. Hehehe...";
                lines[1] = "I say you are getting one step closer to THE END...! The juicy worms, I might say..";
                lines[2] = "Anways, here's something I COOKED UP for you. Hehehehehe. Hope you like it!";
                positiveResponse = "Wormy. HEHEHEHE.";
                negativeResponse = "I guess something else would end up in my stew soon. HEHEHEHEHEHEHEHE!!";
                break;
            case 5:
                lines[0] = "No healing items? Well, we're in the jungle! HAHHAHEEHAHE";
                lines[1] = "No easy way around..unless you have been upgrading, of course.";
                lines[2] = "You can use THIS to cook us some crocodile soup for tonight.";
                positiveResponse = "The more you eat, the CRUNCHIER YOU GET...Heheheheeheiijiwi";
                negativeResponse = "Fine. Only snake stews for you tonight! At least you'll be slimier when you're finished!";
                break;
            case 6:
                lines[0] = "Enjoying the intense music? I'm having a blast while seeing the animals bite your legs off!";
                lines[1] = "I'd care to join in, but I don't want owl wings on the menu..Heheheheeh";
                lines[2] = "What are you exactly though, Quetzal? Someone looks a bit monster himself these days..!";
                positiveResponse = "Well at least you are smart enough to GAMBLE IN THE WILD. HELLEHELLEHELLLALALALALA";
                negativeResponse = "Guess I'll find out...through your corpse! Heheheeehohohoendio";
                break;
            case 7:
                lines[0] = "What's my name? How rude of you to ask my true identity.";
                lines[1] = "But you may call me...The Bum. Or Ms.Bum. Either way, I am..a BUM. HEHELEHOELEOHLOELULU";
                lines[2] = "Let's see how bummed you are with this weapon!";
                positiveResponse = "HEHEHHEEEEHEEHEY! ME LIKE THIS SUCKER..!";
                negativeResponse = "AWWWWW...BABY QUETZAL CAN'T HANDLE THE RISK..! OOWOOOWHOOHOOHOO!";
                break;
            case 8:
                lines[0] = "Defective weapons?? You sure, you say??";
                lines[1] = "HEHEHEHEHHEHEHEHEHHEHEE. I'm glad you noticed.";
                lines[2] = "BUT YOU DON'T HAVE A CHOICE, DO YOU? GAMBLE OR DIE..!";
                positiveResponse = "RICH! HOLOLOLLLOEOL";
                negativeResponse = "CHEAP. You don't deserve to die in my presence. KEKEKEEKEKEK...";
                break;
            case 9:
                lines[0] = "Hi mister! Mister looks like a fine sir, yes he does...Wait..you've heard of that before?";
                lines[1] = "I guess you have a liking for weird and gross stuff, just like me! Hehehehehehe.";
                lines[2] = "For the last two days, you'll be fighting while you are reminded of this nightmare! HAHEHEAHEHAHEHHEHAH";
                positiveResponse = "IT'S A TRAP!!!! HEHEHEH! IT ALWAYS WAS!";
                negativeResponse = "Refusing me to help you out??! BUMMER. HAEHEKJHAHALICEBUMSDDHFJKHJD";
                break;
            case 10:
                lines[0] = "Defeated all the monsters?? Good...good..";
                lines[1] = "What now you say? Well...I say....hehehe...";
                lines[2] = "...HEHEHEHEHEHEHEHEHEH.....IT'S TIME FOR ME..TO COOK YOU INTO STEW..! I DARE YOU TO TAKE THE SWORD AND FIGHT ME..!";
                positiveResponse = "LET'S SEE HOW WELL YOU HAVE GAMBLED...!!!";
                negativeResponse = "WELL...I GUESS YOU'LL END UP LIKE THE MONSTERS YOU KILLED!!!";
                break;
            default:
                lines[0] = "Hmm? Who am I you say? My name is Alice, but you can call me..The Bum.";
                lines[1] = "Love to take credit for everything I didn't do, and return the favor by leaving in silence.";
                lines[2] = "Say, how about you give me a quarter, for old times sake..?";
                positiveResponse = "Hehehehehe. I can finally buy myself gummy worms..Thanks! (Heh. Sucker.)";
                negativeResponse = "Stinky little reptile...I'LL COOK YOU INTO STEW!!!";
                break;
        }
    }
}
