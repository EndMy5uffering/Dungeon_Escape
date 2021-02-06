using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour
{
    public Material[] Shader;

    public float time = 0f;

    public float AnimationSpeed = 1f;

    private int dir = 0;

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
        if (dir != 0)
        {
            time += Time.deltaTime * AnimationSpeed * dir;
            if (time > 1 || time < 0) 
            {
                time = (time >= 1 ? 1 : 0);
                dir = 0;
                SetTime(time);
                return;
            }

            SetTime(time);

        }
    }

    public void SetTime(float time) 
    {
        this.time = time;
        foreach (Material m in Shader)
        {
            m.SetFloat("Time", time);
        }
    }

    public void AnimateIn() 
    {
        dir = -1;
    }

    public bool CanAnimateIn() 
    {
        return !(time > 0);
    }

    public void AnimateOut() 
    {
        dir = 1;
    }

    public bool CanAnimateOut()
    {
        return !(time < 1);
    }

    public float GetTime() 
    {
        return this.time;
    }
}
