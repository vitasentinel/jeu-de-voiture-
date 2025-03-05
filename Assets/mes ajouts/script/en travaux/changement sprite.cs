using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class changementsprite : MonoBehaviour
{
    public ChainManager chainManager;
    public GameObject slime_eau;
    public GameObject slime_feuille;
    // Update is called once per frame
    void Update()
    {
        if (chainManager.typeName == "slime feuille")
        {
            slime_eau.SetActive(false);
            slime_feuille.SetActive(true);
        }
        if (chainManager.typeName == "slime eau")
        {
            slime_feuille.SetActive(false);
            slime_eau.SetActive(true);
        }
    }
}
