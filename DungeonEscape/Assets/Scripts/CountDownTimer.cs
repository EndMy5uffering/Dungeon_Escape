using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    float currentTime = 0;
   public float startingTime = 60;

    public Text timer;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timer.text = currentTime.ToString();
        if(currentTime <= 0)
        {
            timer.text = "Times up";
        }
    }
}
