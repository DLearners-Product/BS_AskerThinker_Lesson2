using UnityEngine;
using UnityEngine.Video;

public class StoryController : MonoBehaviour
{
    //video
    [SerializeField] private VideoPlayer videoPlayer;

    //go
    [SerializeField] private GameObject blackScreen;

    void Start()
    {
        videoPlayer.loopPointReached += CheckOver;
    }

    public void CheckOver(VideoPlayer vp)
    {
        Invoke("ShowBlackScreen", 1f);
    }

    public void ShowBlackScreen()
    {
        blackScreen.SetActive(true);
    }




}
