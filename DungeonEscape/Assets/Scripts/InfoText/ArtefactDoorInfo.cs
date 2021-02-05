using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactDoorInfo : MonoBehaviour
{

    private List<string> ArtefactDoorInfoCards = new List<string>() { "This looks like something that could revive me.", "I should find a way inside." };

    public GameObject Canvas;
    public GameObject Player;
    public GameObject Door;

    public float waitfor = 1.5f;

    private bool showText = true;

    public IEnumerator suspendInfoText() 
    {
        showText = false;
        yield return new WaitForSeconds(waitfor);
        showText = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (showText && !Door.GetComponent<Door>().Open) 
        {
            Canvas.GetComponent<UIManager>().SetInfoBoxCards(ArtefactDoorInfoCards);
            Canvas.GetComponent<UIManager>().NextCardForInfoBox();
            StartCoroutine(suspendInfoText());
        }
        
    }

}
