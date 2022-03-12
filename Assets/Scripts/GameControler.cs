using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    public static GameControler Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    //------------------------------------------------------------------------

    [SerializeField] private GameObject[] floorsPrefabs;
    [SerializeField] private GameObject[] levelState;
    [SerializeField] private sbyte step;

    internal static int score;
    internal static int hScore;

    private float lenghtFloor;
    private GameObject ninja;

    //------------------------------------------------------------------------

    void Start()
    {
        ninja = GameObject.Find("Ninja");
        lenghtFloor = floorsPrefabs[0].GetComponentInChildren<Transform>().Find("Floor").localScale.z;
        score = 0;
        step = 5;

        levelState = new GameObject[]
        {
            floorsPrefabs[0],
            floorsPrefabs[1],
            floorsPrefabs[2],
            floorsPrefabs[3],
            floorsPrefabs[4],
            floorsPrefabs[5]
        };

        for (sbyte i = 0; i < levelState.Length; i++)
        {
            levelState[i].transform.position = new Vector3(0,0, i * lenghtFloor);
            levelState[i] = Instantiate(levelState[i]);
        }
    }

    void Update()
    {

        for (sbyte i = (sbyte)(levelState.Length-1); i >= 0; i--)
        {
            GameObject lclfloor = levelState[i];
            
            sbyte previous = (i == 0) ? (sbyte)(levelState.Length-1) : (sbyte)(i-1);

            if ((lclfloor.transform.position.z + lenghtFloor/2 + 10) < ninja.transform.position.z)
            {
                Destroy(lclfloor);

                if (step >= 6)
                {
                    lclfloor = Instantiate(floorsPrefabs[0]);
                    step = 0;
                }
                else
                {
                    lclfloor = Instantiate(floorsPrefabs[Random.Range(2, floorsPrefabs.Length)]);
                    step++;
                }
                lclfloor.transform.position = new Vector3(0,0, levelState[previous].transform.position.z + lenghtFloor);
                levelState[i] = lclfloor;
            }
        }

        if (NinjaControl.gameOver)
        {
            hScore = (hScore <= score) ? score : hScore;
        }

    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
