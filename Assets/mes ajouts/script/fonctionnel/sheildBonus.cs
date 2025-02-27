using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheildBonus : MonoBehaviour
{
    public ChainManager ChainManager; 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTrigger"))
        {
            ChainManager.sheildBonus = true;
            Destroy(this.gameObject);
        }
    }
}
