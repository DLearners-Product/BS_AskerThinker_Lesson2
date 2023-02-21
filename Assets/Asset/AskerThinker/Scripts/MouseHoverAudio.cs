using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverAudio : MonoBehaviour
{
    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    public void OnMouseEnter()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnMouseExit()
    {
        audioSource.Stop();
    }
}
