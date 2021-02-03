using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    public bool Open = false;
    public float Speed = 1f;

    private bool animate = false;

    public float minAngle = 0;
    public float maxAngle = 90;

    public Vector3 Axis = new Vector3(0, 1, 0);
    private float deltaAngle = 0f;

    private Quaternion State0, State1;

    private void Awake()
    {
        State0 = transform.rotation;
        State1 = transform.rotation * Quaternion.Euler(Axis * maxAngle);
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
        }
    }

    public void DoorOpenClose(bool open) 
    {
        this.Open = open;
        animate = true;
    }

    public void OnInteract(InputAction.CallbackContext context) 
    {
        DoorOpenClose(!Open);
    }
}
