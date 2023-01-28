using UnityEngine;
using TMPro;

public class Balloon : MonoBehaviour
{
    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip balloonFly;
    [SerializeField] private AudioClip whoosh;

    //anim
    [SerializeField] private Animator balloonAnim;

    //inputfield
    [SerializeField] private TMP_InputField answerInputField;

    private void Start()
    {
        PlaySE(balloonFly);
    }

    public void KeepFloating()
    {
        balloonAnim.SetTrigger("KeepFloating");
        answerInputField.gameObject.SetActive(true);
        PlaySE(whoosh);
    }

    public void PlaySE(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
