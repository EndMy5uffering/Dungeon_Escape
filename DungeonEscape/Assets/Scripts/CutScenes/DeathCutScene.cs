using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCutScene : MonoBehaviour
{
    public GameObject Player;
    public GameObject Cam;
    public GameObject PlayerSnapPoint;
    public GameObject CamSnapPoint;
    public GameObject Artefact;
    public GameObject canvas;
    public GameObject ArtefactRespawn;

    private bool playerRotate = false;
    private float playerRot = 0f;
    public float PlayerRotationSpeed = 2f;
    private bool rotDone = false;

    private void Awake()
    {
        CountDownTimer.OnTimesUp += timeUpEvent;
    }

    public void timeUpEvent(int timerID) 
    {
        Player.GetComponent<CharControll>().DisableControlles();
        playerRotate = true;
        rotDone = false;
        Cam.GetComponent<Spring>().enabled = false;
        ArtefactRespawn.transform.position = Player.transform.position;
        ArtefactRespawn.transform.rotation = Player.transform.rotation;
        Player.GetComponent<CharControll>().HeadSwap();
    }

    private void Update()
    {

        if (playerRot >= 90 && !rotDone) 
        {
            playerRotate = false;
            rotDone = true;
            StartCoroutine(DeathAndReset());
        }

        if (playerRotate) 
        {
            float rotAmmount = this.PlayerRotationSpeed * Time.deltaTime;
            Player.transform.RotateAround(Player.transform.position, Player.transform.forward, rotAmmount);
            this.playerRot += rotAmmount;
        }

    }

    public IEnumerator DeathAndReset() 
    {
        canvas.GetComponent<UIManager>().TimerTextOffScreen();
        yield return new WaitForSeconds(0.5f);
        Artefact.transform.position = ArtefactRespawn.transform.position;
        Artefact.transform.rotation = ArtefactRespawn.transform.rotation;
        Player.transform.position = PlayerSnapPoint.transform.position;
        Player.transform.rotation = PlayerSnapPoint.transform.rotation;
        Artefact.SetActive(true);
        Artefact.GetComponent<DissolveScript>().AnimateIn();
        yield return new WaitForSeconds(2f);
        Cam.transform.position = CamSnapPoint.transform.position;
        Cam.transform.rotation = CamSnapPoint.transform.rotation;
        yield return new WaitForSeconds(1f);
        Cam.GetComponent<Spring>().enabled = true;
        Player.GetComponent<CharControll>().EnableControlles();
        Player.GetComponent<CharControll>().ArtefactLost();
        Artefact.GetComponent<Collider>().enabled = true;
        DoReset();
    }

    public void DoReset() 
    {
        playerRotate = false; 
        rotDone = false;
        this.playerRot = 0f;
    }

}
