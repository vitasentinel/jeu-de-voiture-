using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    public int addScore;
    public ScoreManager ScoreManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerTrigger"))
        {
            ScoreManager.AddScore(addScore);
            Destroy(this.gameObject);
        }
        
    }
}