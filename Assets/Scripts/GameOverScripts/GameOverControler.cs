using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverControler : MonoBehaviour
{
    [SerializeField] GameObject ninja;
    [SerializeField] Image[] curtains;
    [SerializeField] Text[] scores;
    [SerializeField] AudioSource[] sounds;

    private float timer;
    private bool transition;
    private bool choiceRetry;
    private bool choiceMenu;

    void Start()
    {
        curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);

        scores[0].text = GameControler.score.ToString();
        scores[1].text = GameControler.hScore.ToString();

        transition = true;
        choiceRetry = false;
        choiceMenu = false;
        sounds[0].volume = 0.3f;
    }

    void Update()
    {
        ninja.transform.position -= new Vector3(0, 30*Time.deltaTime, 0);

        timer += Time.deltaTime;

        if (transition)
        {
            timer += Time.deltaTime;
            if (timer >= 0.1)
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

        if (choiceRetry || choiceMenu)
        {
            curtains[0].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 300);
            curtains[1].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 300);
            sounds[0].volume -= Time.deltaTime / 3;
            if (curtains[0].rectTransform.sizeDelta.y >= Screen.height || curtains[1].rectTransform.sizeDelta.y >= Screen.height)
            {
                curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
                curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
                if (choiceRetry)
                {
                    SceneManager.LoadScene(1);
                }
                else if (choiceMenu)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayRetry()
    {
        choiceRetry = true;
        sounds[1].Play();
    }

    public void PlayMenu()
    {
        choiceMenu = true;
        sounds[1].Play();
    }

    public void ExitButton()
    {
        Application.Quit();
        sounds[1].Play();
    }
}
