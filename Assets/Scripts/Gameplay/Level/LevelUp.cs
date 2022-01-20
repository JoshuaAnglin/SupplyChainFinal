using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SCG.Stats
{ 
    public class LevelUp : MonoBehaviour
    {
        static public int level = 1;
        public Image barImage;
        public static LevelUp instance;
        public float xp;
        public Text Leveltext;
    
        private void Start()
        {
            instance = this;
        }

        public void GetXP(float xp)
        {
            barImage.fillAmount += xp;
        }

    }
}



