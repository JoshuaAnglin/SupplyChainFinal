using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    Pause pause;
    Text word;

    [SerializeField] AudioClip countdown;
    [SerializeField] GameObject centreRing;

    void Start()
    {
        pause = FindObjectOfType<Pause>();
        word = transform.GetChild(0).GetComponent<Text>();
    }

    public void ChangeText(string wd)
    {
        word.text = wd;
    }

    public void startCountdown()
    {
        pause.soundEffects.PlayOneShot(countdown);
    }

    public void Begin()
    {
        centreRing.SetActive(true);
        pause.bgm.gameObject.SetActive(true);
        GameplayUI.status = GameplayUI.gameplayState.unpaused;
        GameplayUI.StatusAction();
    }

    IEnumerator ModifyText()
    {
        yield return new WaitForSeconds(1f);
    }
}
