using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCG.Stats
{

    public class BaseStats : MonoBehaviour
    {
        // creates a slider for the starting level of a character. I have limited it to have a max level of 5. THIS SCRIPT EXISTS ON BOTH PLAYER AND ENEMY
        [Range(1, 5)] [SerializeField]  int startingLevel = 1;
        // Creates a reference to the type of character from the list in "characterClass.cs". For example you will notice on the Player object, this is set to type player and on the enemy object, this is set to type Robot. 
        // This makes sure the player and enemy have the correct values from the "Progression" chart depending on if they are a player or a robot
        [SerializeField] CharacterClass characterClass;
        // Creates a reference to the progression chart
        [SerializeField] Progression progression = null;

        public float GetHealth()
        {
            // Calls the GetHealth method from the Progression script
            return progression.GetHealth(characterClass, startingLevel);
        }
        // need to move (SAM PUT IN THIS PART)
        public void Update()
        {
          if(LevelUp.instance.barImage.fillAmount == 1f)
            {
                startingLevel = 2;
                GetHealth();
            }
        }

    }

   
}




