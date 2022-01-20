using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCG.Stats
{
    public class Machines : MonoBehaviour
    {
        [SerializeField] int idReference;
        [SerializeField] string[] markComplete;
        [SerializeField] string nextQuest;
        [SerializeField] GameObject[] links;

        void QuestUpdater()
        {
            QuestManager.instance.quest.text = nextQuest;
            Destroy(GetComponent<Machines>());
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<IInteractWith>() != null)
            {
                if (collision.transform.GetComponent<Item>().Effect(idReference))
                {
                    foreach(string str in markComplete)
                    {
                        QuestManager.instance.MarkQuestComplete(str);
                    }
                    QuestUpdater();
                    Destroy(collision.gameObject);

                    GameplayUI.inst.questComplete.SetActive(true);
                    Pause.inst.soundEffects.PlayOneShot(GameplayUI.inst.questCompleteSound);

                    if (links != null)
                    {
                        switch(idReference)
                        {
                            case 1:
                                Destroy(links[0]);
                                break;

                            case 2:
                                links[0].SetActive(false);
                                links[1].SetActive(true);
                                break;
                        }
                    }

                    LevelUp.instance.GetXP(0.5f);

                    if (LevelUp.instance.barImage.fillAmount >= 1)
                    {
                        Debug.Log(LevelUp.level);
                        LevelUp.instance.barImage.fillAmount = 0;
                        LevelUp.level++;
                        Debug.Log(LevelUp.level);
                        LevelUp.instance.Leveltext.text = "Level: " + LevelUp.level;
                        Health.instance.GetHealth();
                    }
                }
            }
        }
    }
}
