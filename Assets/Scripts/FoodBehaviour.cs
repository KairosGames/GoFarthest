using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 rotSpeed;

    void Start()
    {
        rotSpeed = new Vector3(0, 180.0f, 0);
    }
    void Update()
    {
        transform.Rotate(rotSpeed*Time.deltaTime);
    }
}
