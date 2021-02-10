using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRevival : MonoBehaviour
{

    private List<List<string>> ICards = new List<List<string>>() { 
        new List<string>() { "This is it!", "With this I'll be able to become human again.", "*Bing*"},
    new List<string>() { "Oh no it dissapeard!", "Where did it go!"},
    new List<string>() { "Huray!" , "I am human once again!", "I cant wait to get out of this place!" }};

    private int count = 0;

    public GameObject cam;
    public GameObject Player;
    public GameObject PlayerSmapPoint;
    public GameObject Canvas;

    private GameObject Artefact;
    private GameObject camPrefTarget;

    public bool playerWasRevied = false;

    private void Awake()
    {
        CharControll.OnRevivalCutScene += OnSceneStart;
    }

    public IEnumerator Animate() 
    {
        yield return new WaitForSeconds(2f);
    }

    public void Cards() 
    {
        switch (count) 
        {
            case 0:
                break;
            case 1:
                this.Artefact.GetComponent<DissolveScript>().AnimateOut();
                break;
            case 2:
                Player.GetComponent<CharControll>().HeadSwap();
                break;
            case 3:
                Player.GetComponent<CharControll>().EnableControlles();
                Player.GetComponent<CharControll>().EnableWayFinder();
                cam.GetComponent<Spring>().SetTarget(camPrefTarget);
                Canvas.GetComponent<UIManager>().TimerTextOnScreen();
                Canvas.GetComponent<CountDownTimer>().start();
                break;
            default:
                break;
        }

        if (count < ICards.Count) {
            StartCoroutine(nextCard());
        }
    }

    private IEnumerator nextCard() 
    {
        yield return new WaitForSeconds(2f);
        Canvas.GetComponent<UIManager>().SetInfoBoxCards(ICards[count++]);
        Canvas.GetComponent<UIManager>().NextCardForInfoBox();
    }

    public void OnSceneStart(GameObject obj) 
    {
        if (!playerWasRevied)
        {
            playerWasRevied = true;
            this.Artefact = obj;
            Player.GetComponent<CharControll>().DisableControlles();
            Player.GetComponent<CharControll>().DisableWayFinder();
            this.camPrefTarget = cam.GetComponent<Spring>().GetTarget();
            cam.GetComponent<Spring>().SetTarget(this.gameObject);
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerSmapPoint.transform.position;
            Player.transform.rotation = PlayerSmapPoint.transform.rotation;
            Player.GetComponent<CharacterController>().enabled = true;
            Canvas.GetComponent<UIManager>().OnInfoBoxCardsEmpty += Cards;
            Canvas.GetComponent<UIManager>().SetInfoBoxCards(ICards[count++]);
            Canvas.GetComponent<UIManager>().NextCardForInfoBox();
        }
        else 
        {
            Player.GetComponent<CharControll>().HeadSwap();
            this.Artefact.GetComponent<DissolveScript>().AnimateOut();
            Canvas.GetComponent<UIManager>().TimerTextOnScreen();
            Canvas.GetComponent<CountDownTimer>().start();
        }
        
    }
}
