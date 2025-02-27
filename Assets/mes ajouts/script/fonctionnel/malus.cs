using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malus : MonoBehaviour
{
    public int minusScore;
    public ScoreManager ScoreManager;
    public ChainManager ChainManager;

    private void OnTriggerEnter(Collider other)
        {
            if (!ChainManager.sheildBonus)
            {
                if(other.CompareTag("PlayerTrigger"))
                {
                    ScoreManager.MinusScore(minusScore);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                ChainManager.sheildBonus = false;
                Destroy(this.gameObject);
            }
            
        
        }
    
}
