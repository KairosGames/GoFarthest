using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioSource audio;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audio.Play();
    }
}
