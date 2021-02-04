using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldInfoText : MonoBehaviour
{
    private List<List<string>> shieldInfo = new List<List<string>>() {
        new List<string>() { "This looks important\nPerhaps i should find more of them?", "This is sample text!" },
        new List<string>() { "This is looking good :D"}};

    public static int ShieldInfo = 0;

    public GameObject Canvas;

    private static GameObject prefTrigger;

    private void Awake()
    {
        Collectable.OnCollect += collected;
    }

    public void collected(GameObject obj) 
    {
        if (obj.tag.Equals("Shield") && ShieldInfo <= 1 && prefTrigger != obj) 
        {
            prefTrigger = obj;
            Canvas.GetComponent<UIManager>().SetInfoBoxCards(shieldInfo[ShieldInfo++]);
            Canvas.GetComponent<UIManager>().NextCardForInfoBox();
        }
    }

}
