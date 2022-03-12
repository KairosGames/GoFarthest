using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] lstAudio;
    [SerializeField] private AudioSource[] lstMusics;
    GameObject ninja;
    NinjaControl ninjaControl;

    private float timer;
    private bool wind;
    private bool music;

    void Start()
    {
        ninja = GameObject.Find("Ninja");
        ninjaControl = ninja.GetComponent<NinjaControl>();
        timer = 0;
        wind = false;
        music = false;
        lstMusics[0].volume = 0.3f;
        lstMusics[1].volume = 0.1f;
    }

    void Update()
    {
        if (!music)
        {
            timer += Time.deltaTime;
        }

        if(!wind && timer >= 1)
        {
            lstMusics[0].Play();
            wind = true;
        }

        if (timer > 3 && !music)
        {
            music = true;
            timer = 0;
            lstMusics[1].Play();
        }

        if (ninjaControl.fall)
        {
            lstMusics[1].volume -= Time.deltaTime/10;
            if (lstMusics[1].volume <= 0)
            {
                lstMusics[1].volume = 0;
            }
        }

        if (NinjaControl.gameOver)
        {
            lstMusics[0].volume -= Time.deltaTime/3;
            if(lstMusics[0].volume <= 0)
            {
                lstMusics[0].volume = 0;
            }
        }
    }

    internal void PlaySnd(sbyte pNumber)
    {
        lstAudio[pNumber].Play();
    }
}
