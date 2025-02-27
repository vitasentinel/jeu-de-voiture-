using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBonus : MonoBehaviour
{
    public CarController carController;
    public int boostspeed = 6;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTrigger"))
        {
            StartCoroutine(Boosting(other.gameObject));
        }
    }
    private IEnumerator Boosting(GameObject target)
    {
        float baseSpeed = carController.MaxSpeed;
        carController.MaxSpeed = boostspeed;
        yield return new WaitForSeconds(2f);
        carController.MaxSpeed = baseSpeed;
    }
}    
