using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    public ScoreManager ScoreManager;
    public string typeName; 
    public int addScore = 0;
    public bool sheildBonus = false;
    Rigidbody rb;
    public comptearebours comptearebours;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }
    private void Update()
    {
        if (comptearebours.unfreezed == true)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

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
