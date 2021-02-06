using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharControll : MonoBehaviour
{
    public AudioSource steps;
    public AudioClip walk;
    public AudioClip sprint;

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

    private Transform scanner;
    private bool grounded = true;
    private bool jumped = false;
    private bool sprinting = false;

    private CharacterController controller;

    public GameObject Head, Skull;
    public GameObject PlayerModel;
    public GameObject ExitDoor;
    public GameObject Canvas;
    private bool Alive = false;
    private bool Revival = false;

    private float waddleTime = 0f;
    public float waddleSpeed = 5f;
    private bool doWaddle = false;
    private Vector2 prefPresed = new Vector2(-1,-1);
    public bool AllowJumping = true;

    public bool DisableControlls = false;

    private bool hasKey = false;

    private int ShieldsCollected = 0;
    private int KeysCollected = 0;
    private int ArtefactCollected = 0;

    private string objective = "Objective:\n-Shields ({SHIELDS}/4)\n-Key ({KEYS}/1)\n-Artefact ({ARTEFACT}/1)";

    public delegate void RevivalCutScene(GameObject artefact);
    public static event RevivalCutScene OnRevivalCutScene;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        scanner = transform.Find("Scanner");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = moveSpeed;

        Collectable.OnCollect += KeyCollected;

        RefreshObjectiveText();
    }

    void Update()
    {
        grounded = Physics.SphereCast(new Ray(scanner.position, Vector3.down), 0.2f);
        applyGravity();
        if (DisableControlls) return;
        moveChar();
        doLook();
        waddle();
    }

    public void OnWalk(InputAction.CallbackContext context) 
    {
        if (DisableControlls) return;
        Vector2 dir = context.ReadValue<Vector2>().normalized;
        this.moveVel = new Vector3(dir.y, this.moveVel.y, dir.x);
        if (!(dir.x == 0.0f && dir.y == 0.0f))
        {
            doWaddle = true;
            if (!steps.isPlaying)
            {
                steps.Play();
            }
        }
        if(dir.x == 0.0f && dir.y == 0.0f)
        {
            doWaddle = false;
            steps.Pause();
        }
        if (!doWaddle) resetWaddle();
 
        prefPresed = dir;
    }

    public void OnLook(InputAction.CallbackContext context) 
    {
        if (DisableControlls) return;
        Vector2 dir = context.ReadValue<Vector2>();
        mouseX += dir.x;
    }

    private void doLook()
    {
        if (DisableControlls) return;
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
        if (sprinting)
        {
            steps.clip = sprint;
            steps.Play();
        }
        else
        {
            steps.clip = walk;
            steps.Play();
        }
    }

    private void moveChar() 
    {
        if (DisableControlls) return;
        float dt = Time.deltaTime;
        Vector3 dir = transform.forward * moveVel.x + transform.up * moveVel.y + transform.right * moveVel.z;
        dir *= currentSpeed * dt; 
        if (controller.enabled) controller.Move(dir);
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        if (!AllowJumping || DisableControlls) return;
        if(!grounded) doJump();
        jumped = true;
    }

    private void doJump() 
    {
        if (DisableControlls) return;
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
        Head.GetComponent<DissolveScript>().AnimateIn();
        Skull.GetComponent<DissolveScript>().AnimateOut();
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

    public void KeyCollected(GameObject obj)
    {
        if (obj.tag.Equals("Key"))
        {
            obj.SetActive(false);
            this.hasKey = true;
            this.KeysCollected++;
            RefreshObjectiveText();
        }
        else if (obj.tag.Equals("Artefact"))
        {
            obj.GetComponent<Collider>().enabled = false;
            OnRevivalCutScene?.Invoke(obj);
            this.ArtefactCollected++;
            RefreshObjectiveText();
        } else if (obj.tag.Equals("Shield")) 
        {
            this.ShieldsCollected++;
            RefreshObjectiveText();
        }
    }

    public void DisableControlles() 
    {
        this.DisableControlls = true;
        moveVel = new Vector3(0,0,0);
        mouseX = 0;
        doWaddle = false;
        resetWaddle();
    }

    public void EnableControlles() 
    {
        this.DisableControlls = false;
    }

    public bool IsDisableControlles() 
    {
        return this.DisableControlls;
    }

    public bool IsHuman() 
    {
        return this.Alive;
    }

    public void RefreshObjectiveText() 
    {
        Canvas.GetComponent<UIManager>().SetObjectiveText(objective.Replace("{KEYS}", this.KeysCollected+"").Replace("{SHIELDS}", this.ShieldsCollected+"").Replace("{ARTEFACT}", this.ArtefactCollected+""));
    }

    public void DisableWayFinder() 
    {
        gameObject.GetComponent<WayFinder>().DisableWayFinder();
    }

    public void EnableWayFinder() 
    {
        gameObject.GetComponent<WayFinder>().EnableWayFinder();
    }

    public void DisablePlayer() 
    {
        DisableControlles();
        DisableWayFinder();
        this.PlayerModel.SetActive(false);
    }

    public void EnablePlayer() 
    {
        EnableControlles();
        EnableWayFinder();
        this.PlayerModel.SetActive(true);
    }

    public bool CanOpenExit() 
    {
        return hasKey && Alive;
    }

}
