using SCG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCG.Stats
{

    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;


        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
        }
        public void Update()
        {
           healthPoints = GetComponent<BaseStats>().GetHealth();
        }
    }
}
