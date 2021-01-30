using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharControll : MonoBehaviour
{
    private Vector3 moveVel = new Vector3(0, 0, 0);
    private float jump = 0.0f;
    public float moveSpeed = 1f;
    public float JumpForce = 1f;
    public float gravity = -1f;
    public float mouseSenitivity = 0.5f;
    public float mouseDempening = 0.99f;
    private float mouseX = 0;
    private float mouseY = 0;

    private Transform scanner;
    private bool grounded = true;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        scanner = transform.Find("Scanner");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        grounded = Physics.SphereCast(new Ray(scanner.position, Vector3.down), 0.5f);
        moveChar();
        applyGravity();
        doLook();
    }

    public void OnWalk(InputAction.CallbackContext context) 
    {
        Vector2 dir = context.ReadValue<Vector2>().normalized;
        this.moveVel = new Vector3(dir.y, this.moveVel.y, dir.x);
    }

    public void OnLook(InputAction.CallbackContext context) 
    {
        Vector2 dir = context.ReadValue<Vector2>();
        mouseX += dir.x;
    }

    private void doLook()
    {
        if (Mathf.Abs(mouseX) < 0.2f)
        {
            mouseX = 0;
        }
        else 
        {
            mouseX *= mouseDempening;
        }

        transform.Rotate(Vector3.up * mouseX * mouseSenitivity * Time.deltaTime);
        
    }

    private void moveChar() 
    {
        float dt = Time.deltaTime;
        Vector3 dir = transform.forward * moveVel.x + transform.up * moveVel.y + transform.right * moveVel.z;
        dir *= moveSpeed * dt;
        controller.Move(dir);
    }

    private void OnJump() 
    {
    
    }

    private void doJump() 
    {
        moveVel.y = JumpForce;
    }

    private void applyGravity() 
    {
        moveVel.y += gravity * Time.deltaTime;
        moveVel.y *= grounded ? 1 : 0;
    }

}
