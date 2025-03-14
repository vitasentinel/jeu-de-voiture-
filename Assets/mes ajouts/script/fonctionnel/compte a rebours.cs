using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;
using System.Transactions;

public class comptearebours : MonoBehaviour

{
    public Text timerText;
    public GameObject timer;
    public float currenttime;
    public string Go = "Go";
    public bool started = false;
    public bool unfreezed = false;
    void Start()
    {
        currenttime = 3;
        StartCoroutine(startrace());
    }

    public IEnumerator startrace()
    {        
        timerText.text = currenttime.ToString();
        yield return new WaitForSeconds(1f);
        currenttime = 2;
        timerText.text = currenttime.ToString();        
        yield return new WaitForSeconds(1f);
        currenttime = 1;
        timerText.text = currenttime.ToString();
        unfreezed = true;
        yield return new WaitForSeconds(1f);
        timerText.text = Go;       
        yield return new WaitForSeconds(1f);
        started = true;
        timer.SetActive(false);
    }
}
