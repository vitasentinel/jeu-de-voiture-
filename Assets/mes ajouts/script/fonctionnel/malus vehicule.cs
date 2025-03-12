using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malus_vehicule : MonoBehaviour
{
    public int minusScore;
    public ScoreManager ScoreManager;
    public ChainManager ChainManager;

    private void OnCollisionEnter(Collision other )
        {
            if (!ChainManager.sheildBonus)
            {
                if(other.gameObject.CompareTag("PlayerTrigger"))
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
