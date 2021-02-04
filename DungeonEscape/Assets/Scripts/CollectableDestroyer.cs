using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDestroyer : MonoBehaviour
{
    private void Awake()
    {
        Collectable.OnCollect += KeyCollected;
    }

    public void KeyCollected(GameObject obj) 
    {
        if (obj.tag.Equals("Key"))
        {
            destroyObj(obj);
        } else if (obj.tag.Equals("Artefact")) 
        {
            GameObject player = GameObject.Find("Player");
            if (player == null) return;
            player.GetComponent<CharControll>().HeadSwap();
            Debug.Log(player.GetComponent<CharControll>().IsHuman());
            destroyObj(obj);
        }
    }

    public void destroyObj(GameObject obj) 
    {
        obj.SetActive(false);
        GameObject.Destroy(obj);
    }
}
