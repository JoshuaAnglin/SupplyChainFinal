using SCG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDMG : MonoBehaviour
{
    public float playerHealth;
   
        
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
            //Health.instance.TakeDamage(50);
        }
    }
}
