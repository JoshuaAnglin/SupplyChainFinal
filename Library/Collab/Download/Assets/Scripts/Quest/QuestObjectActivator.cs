using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;

    public string questToChek;

 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCompletion();
    }
    public void CheckCompletion()
    {
        if(QuestManager.instance.CheckIfComplete(questToChek))
        {
            objectToActivate.SetActive(false);
        }
    }
}
