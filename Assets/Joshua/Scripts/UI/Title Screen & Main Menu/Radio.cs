using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    Animator anim;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.anyKey)
            anim.SetBool("GoToMainMenu", true);
    }

    public void fe()
    {
        audioSource.PlayOneShot(sound);
    }
}
