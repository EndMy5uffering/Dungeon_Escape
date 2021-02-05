using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutScene : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerSnapPoint;
    public GameObject CamTargetPlayer;
    public GameObject Cam;
    public GameObject Canvas;

    private UIManager IUIManager;

    private List<string> cards = new List<string>() { "This is not the prittyest place to wake up in",
        "I should try to find a way out\n...",
        "But wait\n...", "Hold up\n...",
        "I AM DEAD!", "How long have i been here",
        "In any case i need to get out of here.",
        "But i should first try to become human again.",
        "The ARTEFACT i was looking for might do the trick.",
        "I have to find it!"};

    public void Awake()
    {
        setupStage();
    }

    public void Start()
    {
        StartCoroutine(WaitUntilCards());
    }

    public IEnumerator WaitUntilCards()
    {
        yield return new WaitForSeconds(1f);
        IUIManager.SetInfoBoxCards(cards);
        IUIManager.NextCardForInfoBox();
    }

    public IEnumerator WaitUntilCamTarget() 
    {
        yield return new WaitForSeconds(1f);
        Cam.GetComponent<Spring>().SetTarget(this.CamTargetPlayer);
        Player.GetComponent<CharControll>().EnableControlles();
        Player.GetComponent<CharControll>().EnableWayFinder();
        Player.GetComponent<CharControll>().RefreshObjectiveText();
    }

    public void CardsDone() 
    {
        IUIManager.OnInfoBoxCardsEmpty -= CardsDone;
        StartCoroutine(WaitUntilCamTarget());
    }

    private void setupStage() 
    {
        this.IUIManager = Canvas.GetComponent<UIManager>();
        Cam.GetComponent<Spring>().SetTarget(gameObject);
        IUIManager.OnInfoBoxCardsEmpty += CardsDone;
        Player.GetComponent<CharControll>().DisableControlles();
        Player.GetComponent<CharControll>().DisableWayFinder();
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = PlayerSnapPoint.transform.position;
        Player.transform.rotation = PlayerSnapPoint.transform.rotation;
        Player.GetComponent<CharacterController>().enabled = true;
        Cam.transform.position = gameObject.transform.position;
        Cam.transform.rotation = gameObject.transform.rotation;
        this.IUIManager.SetObjectiveText("");
    }
}
