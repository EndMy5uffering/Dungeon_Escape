using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    float currentTime = 0;
    public float startingTime = 60;
    float i = 1;
    public Color newColor;

    public Text timer;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime*i;
        timer.text = currentTime.ToString();
        if(currentTime <= 0)
        {
            timer.text = "00";
            timer.color = new Color(1,0,0,1);
        }
    }

    void stop()
    {
        i = 0;
    }
    void start()
    {
        i = 1;
    }
}
