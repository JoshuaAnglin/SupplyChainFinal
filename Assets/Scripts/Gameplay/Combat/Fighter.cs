using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCG.Stats;

// The script for all the fighting mechanics. THIS SCRIPT DOES NOT DO ANYTHING FOR THE MOMENT
namespace SCG.Combat
{
    public class Fighter : MonoBehaviour
    {
        float weaponDamage = 0f;
        Transform healthBar;

        void Start()
        {
            healthBar = transform.GetChild(0);
        }

        // This automatically executes when the player hits the enemy (part of the interface)
        public void Attack()
        {
            weaponDamage = Random.Range(0, 11);
            var health = GetComponent<Health>();
            health.TakeDamage(weaponDamage);
            
            healthBar.GetComponent<HealthBar>().UpdateHealth();
        }
    }
}