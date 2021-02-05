using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour
{
    public Material[] Shader;

    float time = 0f;

    private void Awake()
    {
        foreach (Material m in Shader) 
        {
            m.SetFloat("Time", time);
        }
    }

    float steper = 0;

    private void Update()
    {
        steper += Time.deltaTime*2f;
        SetTime((0.5f * Mathf.Sin(steper)) + 0.5f);
    }

    public void SetTime(float time) 
    {
        this.time = time;
        foreach (Material m in Shader)
        {
            m.SetFloat("Time", time);
        }
    }

    public float GetTime() 
    {
        return this.time;
    }
}
