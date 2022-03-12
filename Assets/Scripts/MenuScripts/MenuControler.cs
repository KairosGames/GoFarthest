using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    [SerializeField] Image[] curtains;
    [SerializeField] AudioSource[] sounds;
    [SerializeField] GameObject controlsPanel;

    private bool choice;
    private bool transition;
    private float timer;

    void Start()
    {
        curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        sounds[0].volume = 0.3f;
        sounds[1].volume = 0.3f;
        transition = true;
        choice = false;
    }

    void Update()
    {
        if (transition)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                curtains[0].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime * 300);
                curtains[1].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime * 300);
            }
            if (curtains[0].rectTransform.sizeDelta.y <= 0 || curtains[1].rectTransform.sizeDelta.y <= 0)
            {
                curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, 0);
                curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, 0);
                transition = false;
                timer = 0;
            }
        }
        
        if (choice)
        {
            curtains[0].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 300);
            curtains[1].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 300);
            sounds[0].volume -= Time.deltaTime/3;
            sounds[1].volume -= Time.deltaTime/3;
            if (curtains[0].rectTransform.sizeDelta.y >= Screen.height || curtains[1].rectTransform.sizeDelta.y >= Screen.height)
            {
                curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
                curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
                SceneManager.LoadScene(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayButton()
    {
        choice = true;
        sounds[2].Play();
    }

    public void ControlsButton()
    {
        if (controlsPanel.activeInHierarchy)
        {
            controlsPanel.SetActive(false);
        }
        else
        {
            controlsPanel.SetActive(true);
        }
        sounds[3].Play();
    }

    public void ExitButton()
    {
        Application.Quit();
        sounds[2].Play();
    }

}
