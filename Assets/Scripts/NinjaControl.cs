using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NinjaControl : MonoBehaviour
{
    [SerializeField] private float frontSpeed;
    [SerializeField] private float lateralSpeed;
    [SerializeField] private float frtAcc;
    [SerializeField] private float ltrAcc;
    [SerializeField] private float rotate;

    static internal float gameSpeed;

    [SerializeField] Collider[] colliders;
    [SerializeField] AudioManager audioManager;

    private Animator animator;
    private GameObject ninja;

    private bool goingon;
    internal bool sprint;
    internal bool slide;
    internal bool turnright;
    internal bool turnleft;
    internal bool jump;
    private bool topjump;
    internal bool fall;
    static internal bool gameOver;

    private float slidetimer;
    private float falltimer;

    //Cheat
    private bool immortal;
    [SerializeField] private GameObject indicator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ninja = GameObject.Find("ninja");
        goingon = false;
        sprint = false;
        slide = false;
        jump = false;
        topjump = false;
        fall = false;
        gameOver = false;

        slidetimer = 0;
        falltimer = 0;

        transform.position = new Vector3(0, 0.25f, -13.0f);
        frtAcc = 0;
        ltrAcc = 0;
        rotate = 0;
        gameSpeed = 1.0f;

        colliders[0].enabled = true;
        colliders[1].enabled = false;

        //Cheat
        immortal = false;
    }

    void Update()
    {
        
        transform.position += new Vector3(0, 0, frontSpeed * frtAcc * Time.deltaTime * gameSpeed);
        transform.position += new Vector3(lateralSpeed * ltrAcc * Time.deltaTime * gameSpeed, 0, 0);
        ninja.transform.localRotation = new Quaternion(0, rotate, 0, ninja.transform.localRotation.w);
        animator.speed = gameSpeed;

        // Going on running
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && !fall)
        {
            goingon = true;
            animator.SetBool("sprint", true);
        }
        if (goingon)
        {
            frtAcc += Time.deltaTime * 2;
            if (frtAcc >= 1)
            {
                goingon = false;
                frtAcc = 1.0f;
                sprint = true;
            }
        }

        if (sprint)
        {
            // Turn
                // Left
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetBool("turnleft", true);
                turnleft = true;
                ltrAcc -= Time.deltaTime*3;
                rotate -= Time.deltaTime * 3 * (17.5f * Mathf.Deg2Rad) * gameSpeed;
                ltrAcc = (ltrAcc <= -1) ? -1.0f : ltrAcc;
                rotate = (rotate <= -17.5f*Mathf.Deg2Rad) ? -17.5f * Mathf.Deg2Rad : rotate;
            }
            else
            {
                animator.SetBool("turnleft", false);
                turnleft = false;
                if (ltrAcc < 0)
                {
                    ltrAcc += Time.deltaTime*3;
                    ltrAcc = (ltrAcc >= 0) ? 0 : ltrAcc;
                }
                if (rotate < 0)
                {
                    rotate += Time.deltaTime * 3 * (17.5f * Mathf.Deg2Rad) * gameSpeed;
                    rotate = (rotate >= 0) ? 0 : rotate;
                }
            }
                //Right
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetBool("turnright", true);
                turnright = true;
                ltrAcc += Time.deltaTime*3;
                rotate += Time.deltaTime * 3 * (17.5f * Mathf.Deg2Rad) * gameSpeed;
                ltrAcc = (ltrAcc >= 1) ? 1.0f : ltrAcc;
                rotate = (rotate >= 17.5f*Mathf.Deg2Rad) ? 17.5f * Mathf.Deg2Rad : rotate;
            }
            else
            {
                animator.SetBool("turnright", false);
                turnright = false;
                if (ltrAcc > 0)
                {
                    ltrAcc -= Time.deltaTime*3;
                    ltrAcc = (ltrAcc <= 0) ? 0 : ltrAcc;
                }
                if (rotate > 0)
                {
                    rotate -= Time.deltaTime * 3 * (17.5f * Mathf.Deg2Rad) * gameSpeed;
                    rotate = (rotate <= 0) ? 0 : rotate;
                }
            }

            // Slowdown
            if (turnleft || turnright || slide)
            {
                frtAcc -= Time.deltaTime/2.5f;
                frtAcc = (frtAcc <= 0.8) ? 0.8f : frtAcc;
            }
            else
            {
                if (!jump)
                {
                    frtAcc += Time.deltaTime/2.5f;
                    frtAcc = (frtAcc >= 1) ? 1.0f : frtAcc;
                }
            }

            // Cancel
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.D))
            {
                animator.SetBool("turnleft", false);
                animator.SetBool("turnright", false);
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && !jump)
            {
                jump = true;
                animator.SetBool("jump", true);
            }
            if (jump)
            {
                if (!topjump)
                {
                    transform.position += new Vector3(0, (10*Time.deltaTime)* gameSpeed, 0);
                    if (transform.position.y >= 4)
                    {
                        topjump = true;
                        transform.position = new Vector3(transform.position.x, 4.0f, transform.position.z);
                    }
                }
                if (topjump)
                {
                    transform.position += new Vector3(0, -(10*Time.deltaTime)* gameSpeed, 0);
                    if (transform.position.y <= 0.25)
                    {
                        jump = false;
                        topjump = false;
                        animator.SetBool("jump", false);
                        transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
                    }
                }
            }

            // Slide
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !slide)
            {
                animator.SetBool("slide", true);
                slide = true;
            }
            if (slide)
            {
                colliders[0].enabled = false;
                colliders[1].enabled = true;
                slidetimer += Time.deltaTime;
                if (slidetimer >= 1)
                {
                    animator.SetBool("slide", false);
                    slide = false;
                    slidetimer = 0;
                    colliders[1].enabled = false;
                    colliders[0].enabled = true;
                }
            }
        }


        // Fall
        if (fall)
        {
            falltimer += Time.deltaTime;
            frtAcc -= (Time.deltaTime/2) * gameSpeed;
            frtAcc = (frtAcc <= 0) ? 0 : frtAcc;

            if (ltrAcc > 0)
            {
                ltrAcc -= Time.deltaTime / 2;
                ltrAcc = (ltrAcc <= 0) ? 0 : ltrAcc;
            }
            else if (ltrAcc < 0)
            {
                ltrAcc += Time.deltaTime/2;
                ltrAcc = (ltrAcc >= 0) ? 0 : ltrAcc;
            }

            if (transform.position.y > 0.25)
            {
                transform.position -= new Vector3(0, Time.deltaTime * 8, 0);
            }

            if (falltimer >= 0.8 && transform.position.y > -1.26)
            {
                transform.position -= new Vector3(0, Time.deltaTime*4, 0);

                if (transform.position.y <= -1.26)
                {
                    transform.position = new Vector3(transform.position.x, -1.26f, transform.position.z);
                }
            }

            if (falltimer > 3)
            {
                gameOver = true;
            }
        }


        // Limits
        // Y
        if (transform.position.y <= 0.25 && !fall)
        {
            transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
        }
            // X
        if (transform.position.x >= 9)
        {
            transform.position = new Vector3(9.0f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(-9.0f, transform.position.y, transform.position.z);
        }


        // QuitGame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        //Cheat
        if (Input.GetKeyDown(KeyCode.I))
        {
            immortal = !immortal;
        }
        if (immortal)
        {
            if (!indicator.activeInHierarchy)
            {
                indicator.SetActive(true);
            }
        }
        else
        {
            if (indicator.activeInHierarchy)
            {
                indicator.SetActive(false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obs") && !immortal)
        {
            animator.SetBool("fall", true);
            fall = true;
            sprint = false;
            turnleft = false;
            turnright = false;
            jump = false;
            slide = false;
        }

        if (other.CompareTag("food"))
        {
            GameControler.score += 5;
            audioManager.PlaySnd(0);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("nigiri"))
        {
            GameControler.score += 10;
            audioManager.PlaySnd(1);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("plate"))
        {
            gameSpeed += 0.1f;
            audioManager.PlaySnd(2);
            other.gameObject.SetActive(false);
        }
    }
}
