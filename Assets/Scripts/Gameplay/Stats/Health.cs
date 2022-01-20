using SCG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCG.Stats
{
    public class Health : MonoBehaviour
    {
        public static Health instance;
        public float maxHealthPoints;
        public float currentHealthPoints;

        private void Start()
        {
            instance = this;
            GetHealth();
            // Returns the health value from the chart 
           // maxHealthPoints = GetComponent<BaseStats>().GetHealth();
            //currentHealthPoints = maxHealthPoints;
        }

        public void TakeDamage(float damage)
        {
            // Makes sure the health cannot go below 0
            currentHealthPoints = Mathf.Max(currentHealthPoints - damage, 0);
            if (currentHealthPoints <= 0) gameObject.SetActive(false);
        }

        public float GetFraction()
        {
            return currentHealthPoints / maxHealthPoints;
        }

        public void GetHealth()
        {
            maxHealthPoints = GetComponent<BaseStats>().GetHealth();
            currentHealthPoints = maxHealthPoints;
        }

        void Update()
        {
            if (LevelUp.instance.barImage.fillAmount == 1)
            {
                GetHealth();
            }
        }
    }
}