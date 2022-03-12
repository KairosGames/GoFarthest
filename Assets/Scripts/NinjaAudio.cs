using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAudio : MonoBehaviour
{

    [SerializeField] private AudioSource[] lstSounds1;
    [SerializeField] private AudioSource[] lstSounds2;

    void Start()
    {

    }

    void Update()
    {
        if (lstSounds1[lstSounds1.Length-1].pitch != NinjaControl.gameSpeed)
        {
            for (sbyte i = 0; i < lstSounds1.Length; i++)
            {
                lstSounds1[i].pitch = NinjaControl.gameSpeed;
            }
        }
    }

    void PlayFootStep1()
    {
        lstSounds1[0].Play();
    }

    void PlayFootStep2()
    {
        lstSounds1[1].Play();
    }

    void PlayJump()
    {
        lstSounds1[2].Play();
    }

    void PlaySlide()
    {
        lstSounds1[3].Play();
    }

    void PlayHit()
    {
        lstSounds2[0].Play();
    }

    void PlayFall()
    {
        lstSounds2[1].Play();
    }

    void PlayFail()
    {
        lstSounds2[2].Play();
    }
}
