using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    Pause pause;
    Startup startUp;

    [SerializeField] GameObject beforeStartupInfo;
    public GameObject questComplete;
    [SerializeField] Animator characterInfo;
    [SerializeField] AudioClip emergencyQuestInfo;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip youWinSound;
    [SerializeField] AudioClip characterInfoSound;
    public AudioClip questCompleteSound;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject youWin;
    [SerializeField] Text time;

    static public GameplayUI inst;

    public enum gameplayState
    {
        questInfo,
        startup,
        paused,
        unpaused
    }

    static public gameplayState status;

    float minutes = 5;
    float seconds = 00;
    bool CIOn;

    void Awake()
    {
        startUp = FindObjectOfType<Startup>();
        pause = GetComponent<Pause>();
        inst = this;
    }

    void Start()
    {
        beforeStartupInfo.SetActive(true);
        pause.soundEffects.PlayOneShot(emergencyQuestInfo);
        status = gameplayState.questInfo;
        TimeSystem(seconds == 60, 1, 00);
        StatusAction();

    }

    void Update()
    {
        if (status == gameplayState.questInfo && Input.GetKeyDown(KeyCode.Space))
        {
            if(pause.soundEffects.isPlaying) pause.soundEffects.Stop();
            beforeStartupInfo.SetActive(false);
            status = gameplayState.startup;
            startUp.GetComponent<Animator>().SetBool("startupPlay", true);
        }

        GameIs(minutes <= 0 && seconds <= 0, 1);
        CharacterInfo();

        // Timer
        seconds -= Time.deltaTime;

        TimeSystem(seconds <= -1, -1, 59);
        TimeSystem(seconds >= 60, 1, 00);

        if (minutes <= 0 && seconds <= 0) seconds = minutes = 0;

        time.text = minutes.ToString("00") + ":" + (Mathf.CeilToInt(seconds)).ToString("00");
    }

    static public void StatusAction()
    {
        switch (status)
        {
            case gameplayState.paused:
                GameplayConditions(0, CursorLockMode.None, true);
                break;

            case gameplayState.unpaused:
                GameplayConditions(1, CursorLockMode.Locked, false);
                break;

            default:
                GameplayConditions(0, CursorLockMode.Locked, false);
                break;
        }
    }

    static void GameplayConditions(int time, CursorLockMode cursor, bool visibility)
    {
        Time.timeScale = time;
        Cursor.lockState = cursor;
        Cursor.visible = visibility;
    }

    public void GameIs(bool cond, int Decider)
    {
        switch(Decider)
        {
            case 1:
                GameplayOver(cond, gameOver, gameOverSound);
                break;

            case 2:
                GameplayOver(cond, youWin, youWinSound);
                break;
        }
    }

    void TimeSystem(bool Decider, int newMin, int newSec)
    {
        if (Decider)
        {
            minutes += newMin;
            seconds = newSec;
        }
    }

    void CharacterInfo()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CIOn = characterInfo.GetBool("CIDisplayed");

            characterInfo.SetBool("CIDisplayed", !CIOn);
            pause.soundEffects.PlayOneShot(characterInfoSound);
        }
    }

    void GameplayOver(bool condition, GameObject state, AudioClip sound)
    {
        if (condition && state.activeSelf == false)
        {
            pause.bgm.Stop();
            pause.bgm.PlayOneShot(sound);
            state.SetActive(true);
            status = gameplayState.paused;
            StatusAction();
        }
    }
}