using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    public ScoreManager ScoreManager;
    public string typeName; 
    public int addScore = 0;
    public bool sheildBonus = false;
    
    
    

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("slime"))
        {
            if (other.CompareTag(typeName))
            {
                addScore += 1;
                ScoreManager.AddScore(addScore);
                
            }
            else
            {
                typeName = other.tag;
                addScore = 0;
            }
        }
        
    }
}
