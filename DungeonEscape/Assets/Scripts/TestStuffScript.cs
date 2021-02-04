using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestStuffScript : MonoBehaviour
{

    public GameObject textbox, target0, target1;

    private bool t0 = true;

    public GameObject Canvas;

    public int c = 0;

    private void Awake()
    {
        Canvas.GetComponent<UIManager>().InfoBoxOnScreen();
        
    }

    public void OnInteract(InputAction.CallbackContext context) 
    {
        c++;
        Canvas.GetComponent<UIManager>().NextTextAnimationInfoBox("Index: " + c);
    }
}
