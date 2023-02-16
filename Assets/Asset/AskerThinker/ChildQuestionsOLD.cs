using TMPro;
using UnityEngine;

public class ChildQuestionsOLD : MonoBehaviour
{
    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClick;

    //text
    [SerializeField] private TextMeshProUGUI q1;
    [SerializeField] private TextMeshProUGUI q2;
    [SerializeField] private TextMeshProUGUI q3;
    [SerializeField] private TextMeshProUGUI q4;

    //GO
    [SerializeField] private GameObject instructionOverlay;

    // Start is called before the first frame update
    void Start()
    {
        q1.text = PlayerPrefs.GetString("1");
        q2.text = PlayerPrefs.GetString("2");
        q3.text = PlayerPrefs.GetString("3");
        q4.text = PlayerPrefs.GetString("4");
    }


    public void OnClickSkipButton()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();

        instructionOverlay.SetActive(false);
    }
}
