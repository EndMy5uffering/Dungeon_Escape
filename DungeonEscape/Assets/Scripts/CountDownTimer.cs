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
    public Color baseColor;

    public GameObject timer;

    public int TimerID = 0;

    public delegate void TimesUp(int TimerID);
    public static event TimesUp OnTimesUp;
    
    void Start()
    {
        resetTimer();
    }

    void Update()
    {
        if (i != 0) 
        {
            currentTime -= Time.deltaTime * i;
            timer.GetComponent<Text>().text = ((int)currentTime) + "";
            if (currentTime <= 0)
            {
                timer.GetComponent<Text>().text = "00";
                timer.GetComponent<Text>().color = new Color(1, 0, 0, 1);
                i = 0;
                OnTimesUp?.Invoke(TimerID);
            }
        }
    }

    public void stop()
    {
        i = 0;
    }

    public void start()
    {
        i = 1;
    }

    public void resetTimer() 
    {
        i = 0;
        currentTime = startingTime;
        timer.GetComponent<Text>().text = "" + currentTime;
        timer.GetComponent<Text>().color = new Color(1, 1, 1, 1);
    }
}
