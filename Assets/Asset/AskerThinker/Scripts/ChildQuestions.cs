using TMPro;
using UnityEngine;

public class ChildQuestions : MonoBehaviour
{
    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClick;

    //GO
    [SerializeField] private GameObject instructionOverlay;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void OnClickSkipButton()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();

        instructionOverlay.SetActive(false);
    }
}
