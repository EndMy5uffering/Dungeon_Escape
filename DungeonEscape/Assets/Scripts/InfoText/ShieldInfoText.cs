using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldInfoText : MonoBehaviour
{
    private List<List<string>> shieldInfo = new List<List<string>>() {
        new List<string>() { "This looks important\nPerhaps i should find more of them?" },
        new List<string>() { "Good!"},
        new List<string>() { "One step closer to beeing releasd from this place."},
        new List<string>() { "Now we are getting somewhere."}};

    public static int ShieldInfoCount = 0;

    public GameObject Canvas;

    private static GameObject prefTrigger;

    private void Awake()
    {
        Collectable.OnCollect += collected;
    }

    public void collected(GameObject obj) 
    {
        if (obj.tag.Equals("Shield") && ShieldInfoCount < shieldInfo.Count && prefTrigger != obj) 
        {
            prefTrigger = obj;
            Canvas.GetComponent<UIManager>().SetInfoBoxCards(shieldInfo[ShieldInfoCount++]);
            Canvas.GetComponent<UIManager>().NextCardForInfoBox();
        }
    }

}
