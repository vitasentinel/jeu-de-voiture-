using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malus : MonoBehaviour
{
    public int minusScore;
    public ScoreManager ScoreManager;
    public ChainManager ChainManager;
    public CarController carController;
    public int slowspeed = 1;

    private void OnTriggerEnter(Collider other)
        {
            if (!ChainManager.sheildBonus)
            {
                if(other.CompareTag("PlayerTrigger"))
                {
                    ScoreManager.MinusScore(minusScore);
                    Destroy(this.gameObject);
                    StartCoroutine(Slow(other.gameObject));
                }
            }
            else
            {
                ChainManager.sheildBonus = false;
                Destroy(this.gameObject);
            }
            
        
        }
    private IEnumerator Slow(GameObject target)
    {
        float baseSpeed = carController.MaxSpeed;
        Debug.Log(baseSpeed);
        carController.MaxSpeed = slowspeed;
        yield return new WaitForSeconds(2f);
        carController.MaxSpeed = baseSpeed;
    }

}
