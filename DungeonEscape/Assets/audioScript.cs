using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class audioScript : MonoBehaviour
{
    System.Random rnd;
    public AudioSource audioSource;
    public List<AudioClip> clips;
    int cur = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            int i = cur;
            while (i == cur)
            {
                i = rnd.Next(clips.Count);
            }

            cur = i;
            audioSource.clip = clips[cur];     
            audioSource.Play();
        }
    }

 
}
