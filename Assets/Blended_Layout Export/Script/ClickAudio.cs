using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAudio : MonoBehaviour, IPointerDownHandler
{
    public AudioSource audioSource;
    public AudioClip clip;

    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}