using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Open = false;
    public float Speed = 1f;

    private bool animate = false;

    public float minAngle = 0;
    public float maxAngle = 90;

    public Vector3 Axis = new Vector3(0, 1, 0);
    private float deltaAngle = 0f;

    public int DoorID = 0;

    private Quaternion State0, State1;

    public delegate void DoorIsOpening(int DoorID);
    public static event DoorIsOpening OnDoorOpening;

    public delegate void DoorIsOpen(int DoorID);
    public static event DoorIsOpen OnDoorIsOpen;

    public delegate void DoorIsCloseing(int DoorID);
    public static event DoorIsCloseing OnDoorCloseing;

    public delegate void DoorIsClosed(int DoorID);
    public static event DoorIsClosed OnDoorIsClosed;

    private void Awake()
    {
        State0 = transform.rotation;
        State1 = transform.rotation * Quaternion.Euler(Axis * maxAngle);
        CollectableHolder.OnFull += DoorOpen;
    }

    void Update()
    {
        if (animate) Animate();
    }

    private void Animate() 
    {
        gameObject.transform.Rotate(Axis, Speed * Time.deltaTime * (Open ? 1 : -1));
        deltaAngle += Speed * Time.deltaTime;

        if (deltaAngle >= maxAngle || deltaAngle <= minAngle)
        {
            deltaAngle = 0;
            animate = false;
            transform.rotation = Open ? State1 : State0;

            if (Open)
            {
                OnDoorIsOpen?.Invoke(this.DoorID);
            }
            else 
            {
                OnDoorIsClosed?.Invoke(this.DoorID);
            }

        }
    }

    public void DoorOpen() 
    {
        DoorOpenClose(true);
    }

    public void DoorClose() 
    {
        DoorOpenClose(false);
    }

    public void DoorOpenClose(bool open) 
    {
        this.Open = open;
        animate = true;

        if (Open)
        {
            OnDoorOpening?.Invoke(this.DoorID);
        }
        else 
        {
            OnDoorCloseing?.Invoke(this.DoorID);
        }

    }
}
