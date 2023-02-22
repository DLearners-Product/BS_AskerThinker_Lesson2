using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerControls : MonoBehaviour
{
    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClick;

    //sprite
    [SerializeField] private Sprite spritePlay;
    [SerializeField] private Sprite spritePause;

    //button
    [SerializeField] private Button btnPlayPause;

    //anim
    [SerializeField] private Animator videoPlayerControlsAnim;

    //videoplayer
    [SerializeField] private VideoPlayer videoPlayer;

    private bool isControlsVisible;
    private bool isVideoPlaying;

    private void Start()
    {
        isControlsVisible = false;
        isVideoPlaying = true;
    }

    public void ShowHideVideoControls()
    {
        if (isControlsVisible)
        {
            videoPlayerControlsAnim.SetBool("Show", false);
            isControlsVisible = false;
        }
        else
        {
            videoPlayerControlsAnim.SetBool("Show", true);
            isControlsVisible = true;
        }

        audioSource.clip = buttonClick;
        audioSource.Play();
    }

    public void OnClickPlayPauseButton()
    {
        if (isVideoPlaying)
        {
            videoPlayer.Pause();
            btnPlayPause.image.sprite = spritePlay;
            isVideoPlaying = false;
        }
        else
        {
            videoPlayer.Play();
            btnPlayPause.image.sprite = spritePause;
            isVideoPlaying = true;
        }
    }

    public void OnClickRestartButton()
    {
        videoPlayer.time = 0;
        videoPlayer.Play();
    }

    public void OnClickBackwardSeekButton()
    {
        double elapsedTime = videoPlayer.time;
        videoPlayer.time = elapsedTime - 5f;
    }

    public void OnClickForwardSeekButton()
    {
        double elapsedTime = videoPlayer.time;
        videoPlayer.time = elapsedTime + 5f;
    }
}
