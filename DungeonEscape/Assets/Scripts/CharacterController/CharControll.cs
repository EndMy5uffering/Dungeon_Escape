using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharControll : MonoBehaviour
{
    private Vector3 moveVel = new Vector3(0, 0, 0);
    private float jump = 0.0f;
    public float moveSpeed = 1f;
    public float SprintSpeed = 2f;
    private float currentSpeed = 1f;
    public float JumpForce = 1f;
    public float gravity = -1f;
    public float mouseSenitivity = 0.5f;
    public float mouseDempening = 0.99f;
    private float mouseX = 0;
    private float mouseY = 0;

    private Transform scanner;
    private bool grounded = true;
    private bool jumped = false;
    private bool sprinting = false;

    private CharacterController controller;

    public GameObject Head, Skull;
    public GameObject PlayerModel;
    private bool Alive = false;

    private float waddleTime = 0f;
    public float waddleSpeed = 5f;
    public bool doWaddle = false;
    private Vector2 prefPresed = new Vector2(-1,-1);

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        scanner = transform.Find("Scanner");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        grounded = Physics.SphereCast(new Ray(scanner.position, Vector3.down), 0.2f);
        moveChar();
        applyGravity();
        doLook();
        waddle();
    }

    public void OnWalk(InputAction.CallbackContext context) 
    {
        Vector2 dir = context.ReadValue<Vector2>().normalized;
        this.moveVel = new Vector3(dir.y, this.moveVel.y, dir.x);
        if (!(dir.x == 0.0f && dir.y == 0.0f))
        {
            doWaddle = true;
        }
        if(dir.x == 0.0f && dir.y == 0.0f)
        {
            doWaddle = false;
        }
        if (!doWaddle) resetWaddle();
        prefPresed = dir;
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

    public void onSprint(InputAction.CallbackContext context) 
    {
        sprinting = context.ReadValueAsButton();
        currentSpeed = sprinting ? SprintSpeed : moveSpeed;
    }

    private void moveChar() 
    {
        float dt = Time.deltaTime;
        Vector3 dir = transform.forward * moveVel.x + transform.up * moveVel.y + transform.right * moveVel.z;
        dir *= currentSpeed * dt;
        controller.Move(dir);
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
       if(!grounded) doJump();
       jumped = true;
        HeadSwap();
    }

    private void doJump() 
    {
        moveVel.y = JumpForce;
    }

    private void applyGravity() 
    {
        moveVel.y += gravity * Time.deltaTime;
        if (!jumped) moveVel.y *= grounded ? 1 : 0;

        if (grounded) jumped = false;
    }

    public void HeadSwap() 
    {
        Alive = !Alive;
        Head.SetActive(Alive);
        Skull.SetActive(!Alive);
    }

    private void waddle() 
    {
        if (doWaddle) 
        {
            waddleTime += waddleSpeed * Time.deltaTime;
            float t = (Mathf.Sin(waddleTime) / 2.0f) + 0.5f;
            float x = PlayerModel.transform.rotation.eulerAngles.x;
            float y = PlayerModel.transform.rotation.eulerAngles.y;
            float z = Mathf.Lerp(-10, 10, t);
            PlayerModel.transform.rotation = Quaternion.Euler(x, y, z);
        }
    }

    private void resetWaddle() 
    {
        waddleTime = 0f;
        float x = PlayerModel.transform.rotation.eulerAngles.x;
        float y = PlayerModel.transform.rotation.eulerAngles.y;
        float z = 0;
        PlayerModel.transform.rotation = Quaternion.Euler(x, y, z);
    }

}
