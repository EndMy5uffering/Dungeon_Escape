using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinAreaEntered : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Player;
    public GameObject PlayerSnapTarget;
    public GameObject CamSnapPoint;
    public GameObject Cam;

    private List<string> endCards = new List<string>() {
        "THE END?"
    };

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<CharControll>().DisableControlles();
        Canvas.GetComponent<UIManager>().TimerTextOffScreen();
        Canvas.GetComponent<CountDownTimer>().stop();
        Canvas.GetComponent<CountDownTimer>().resetTimer();
        Player.GetComponent<CharControll>().DisableControlles();
        Player.GetComponent<CharControll>().DisableWayFinder();
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = PlayerSnapTarget.transform.position;
        Player.transform.rotation = PlayerSnapTarget.transform.rotation;
        Cam.GetComponent<Spring>().enabled = false;
        Cam.transform.position = CamSnapPoint.transform.position;
        Cam.transform.rotation = CamSnapPoint.transform.rotation;

        Canvas.GetComponent<UIManager>().SetInfoBoxCards(endCards);
        Canvas.GetComponent<UIManager>().NextCardForInfoBox();
        Canvas.GetComponent<UIManager>().OnInfoBoxCardsEmpty += switchToEndScreen;
    }

    public void switchToEndScreen() 
    {

        SceneManager.LoadScene(2);
    
    }

}
