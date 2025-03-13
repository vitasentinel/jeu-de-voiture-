using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBonus : MonoBehaviour
{
    public CarController carController;
    public int boostSpeed = 6;
    public int baseSpeed = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTrigger"))
        {
            StartCoroutine(Boosting(other.gameObject));
            Destroy(this.gameObject);
        }
    }
    private IEnumerator Boosting(GameObject target)
    {
        carController.MaxSpeed = boostSpeed;
        yield return new WaitForSeconds(2f);
        carController.MaxSpeed = baseSpeed;
    }
}    
