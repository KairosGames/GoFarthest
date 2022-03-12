using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Display : MonoBehaviour
{
    [SerializeField] Text[] lstTexts;
    [SerializeField] Image[] lstCurtains;

    private float timer;
    private bool transition;

    void Start()
    {
        lstCurtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height/2);
        lstCurtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height/2);
        transition = true;
    }

    void Update()
    {
        lstTexts[0].text = GameControler.score.ToString();
        lstTexts[1].text = ((sbyte)(((NinjaControl.gameSpeed-1)*10)+1)).ToString();

        if (transition)
        {
            timer += Time.deltaTime;

        }
        if (timer >= 0.5 && transition && !NinjaControl.gameOver)
        {
            lstCurtains[0].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime*300);
            lstCurtains[1].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime*300);

            if(lstCurtains[0].rectTransform.sizeDelta.y <= 1 || lstCurtains[1].rectTransform.sizeDelta.y <= 1)
            {
                lstCurtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, 0);
                lstCurtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, 0);
                transition = false;
                timer = 0;
            }
        }

        if (NinjaControl.gameOver)
        {
            lstCurtains[0].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 300);
            lstCurtains[1].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 300);
            if (lstCurtains[0].rectTransform.sizeDelta.y >= Screen.height / 2 || lstCurtains[1].rectTransform.sizeDelta.y >= Screen.height / 2)
            {
                lstCurtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
                lstCurtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
                SceneManager.LoadScene(2);
            }
        }

    }
}
