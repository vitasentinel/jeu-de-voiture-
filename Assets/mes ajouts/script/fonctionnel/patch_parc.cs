using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patch_parc : MonoBehaviour
{
    public GameObject zone1;
    public GameObject zone2;
    public GameObject zone3;
    public GameObject zone4;
    public GameObject zone5;
    public GameObject zone6;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerTrigger")
        {
            zone1.SetActive(false);
            zone2.SetActive(false);
            zone3.SetActive(false);
            zone4.SetActive(true);
            zone5.SetActive(true);
            zone6.SetActive(true);
            
        }
    }
}
