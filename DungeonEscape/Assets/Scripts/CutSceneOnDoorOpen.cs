﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CutSceneOnDoorOpen : MonoBehaviour
{
    public Camera cam;
    public GameObject Player;

    private GameObject prefTarget;
    private float prefDrag;
    private float prefStrength;

    private Spring camSpring;

    private bool Started = false;
    private bool Skiped = false;

    public GameObject Canvas;

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
        Started = true;
        Player.GetComponent<CharControll>().DisableControlles();
        camSpring.SetTarget(this.gameObject);
        cam.transform.position = transform.position + new Vector3(0, 10, 0);
        camSpring.SetDrag(0.001f);
        camSpring.SetStrength(0.005f);
        Canvas.GetComponent<UIManager>().SkipTextOnScreen();
    }

    public void backToPlayer(int DoorID) 
    {
        Started = false;
        if (Skiped) return;
        if (DoorID == 0) 
        {
            resetPlayerCam();
        }
    }

    private void resetPlayerCam() 
    {
        camSpring.SetDrag(prefDrag);
        camSpring.SetStrength(prefStrength);
        camSpring.SetTarget(prefTarget);
        Player.GetComponent<CharControll>().EnableControlles();
        cam.transform.position = prefTarget.transform.position;
        cam.transform.rotation = prefTarget.transform.rotation;
        Canvas.GetComponent<UIManager>().SkipTextOffScreen();
    }

    public void SkipCutScene(InputAction.CallbackContext context) 
    {
        if (!Started || Skiped) return;
        Skiped = true;
        Started = false;
        resetPlayerCam();

    }
}
