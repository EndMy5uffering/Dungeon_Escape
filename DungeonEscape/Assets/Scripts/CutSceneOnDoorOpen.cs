using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneOnDoorOpen : MonoBehaviour
{
    public Camera cam;
    public GameObject Player;

    private GameObject prefTarget;
    private float prefDrag;
    private float prefStrength;

    private Spring camSpring;

    private void Awake()
    {
        CollectableHolder.OnFull += fullTrigger;
        this.camSpring = cam.GetComponent<Spring>();
        this.prefTarget = camSpring.GetTarget();
        this.prefDrag = camSpring.GetDrag();
        this.prefStrength = camSpring.GetStrength();
        Door.OnDoorIsOpen += backToPlayer;
    }

    public void fullTrigger()
    {
        Player.GetComponent<CharControll>().DisableControlles();
        camSpring.SetTarget(this.gameObject);
        cam.transform.position = transform.position + new Vector3(0, 10, 0);
        camSpring.SetDrag(0.001f);
        camSpring.SetStrength(0.005f);
    }

    public void backToPlayer(int DoorID) 
    {
        if (DoorID == 0) 
        {
            camSpring.SetDrag(prefDrag);
            camSpring.SetStrength(prefStrength);
            camSpring.SetTarget(prefTarget);
            Player.GetComponent<CharControll>().EnableControlles();
            cam.transform.position = prefTarget.transform.position;
            cam.transform.rotation = prefTarget.transform.rotation;
        }
    }
}
