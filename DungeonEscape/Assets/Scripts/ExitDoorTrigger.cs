using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorTrigger : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject ExitDoor;

    private List<string> ICards = new List<string>() { "It woun't open if i dont have a key", "It should lay arround somewhere..." };
    private List<string> ICards2 = new List<string>() { "I can't go out looking like this!", "I should try to find a way to become less dead first." };

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) 
        {
            if (other.gameObject.GetComponent<CharControll>().CanOpenExit())
            {
                this.ExitDoor.GetComponent<Door>().DoorOpen();
            }
            else 
            {
                if (other.gameObject.GetComponent<CharControll>().IsHuman())
                {
                    Canvas.GetComponent<UIManager>().SetInfoBoxCards(ICards);
                    Canvas.GetComponent<UIManager>().NextCardForInfoBox();
                }
                else
                {
                    Canvas.GetComponent<UIManager>().SetInfoBoxCards(ICards2);
                    Canvas.GetComponent<UIManager>().NextCardForInfoBox();
                }
            }
        }
    }

}
