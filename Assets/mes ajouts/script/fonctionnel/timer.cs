using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class timer : MonoBehaviour
{
    public float startMinute;
    public Text timerText;
    public float currenttime;

    
    void Start()
    {
        currenttime = startMinute;
    }

    void Update()
    {
        currenttime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currenttime);
        timerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
    }
}
