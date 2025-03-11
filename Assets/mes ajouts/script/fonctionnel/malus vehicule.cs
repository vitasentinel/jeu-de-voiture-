using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malus_vehicule : MonoBehaviour
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
                    
                }
            }
            else
            {
                ChainManager.sheildBonus = false;
                
            }
            
        
        }
    
}
