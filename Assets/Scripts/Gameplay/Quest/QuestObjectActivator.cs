using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{

    Vector3 abovePosition;
    public string questToChek;

    // Update is called once per frame
    void Update()
    {
        CheckCompletion();
    }
    public void CheckCompletion()
    {
        if(QuestManager.instance.CheckIfComplete(questToChek))
        {
            transform.Translate(transform.up * 2 * Time.deltaTime);
        }
    }

}
