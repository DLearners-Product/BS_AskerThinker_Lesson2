using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuessingActivity : MonoBehaviour
{
    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fall;
    //[SerializeField] private AudioClip whoosh;
    [SerializeField] private AudioClip balloonPop;

    //text
    [SerializeField] private TextMeshProUGUI[] guessAnswers;

    //inputfield
    [SerializeField] private TMP_InputField[] guessInputField;

    //button
    [SerializeField] private Button btnSubmit;

    //GO
    [SerializeField] private GameObject saved;
    [SerializeField] private GameObject popTheBalloon;
    [SerializeField] private GameObject[] questionBalloons;

    private int i = 0;
    private Dictionary<int, string> childsGuessAnswers;

    private void Start()
    {
        guessInputField[i].onValueChanged.AddListener(delegate { OnGuessEnteredCheck(); });

        childsGuessAnswers = new Dictionary<int, string>();
    }

    public void OnGuessEnteredCheck()
    {
        if (guessInputField[i].text.Length > 0)
        {
            btnSubmit.gameObject.SetActive(true);
        }
        else
        {
            btnSubmit.gameObject.SetActive(false);
        }
    }

    public void OnClickSubmitButton()
    {
        //store the answer
        childsGuessAnswers.Add(i + 1, guessAnswers[i].text);

        saved.SetActive(true);

        //show answer stored confirmation to child
        Invoke("Save", 1f);


    }

    public void Save()
    {
        guessInputField[i].GetComponent<Animator>().SetTrigger("Disappear");

        //ask child to pop the balloon
        popTheBalloon.SetActive(true);
    }

    public void OnClickBalloon()
    {
        //balloon pop
        PlaySE(balloonPop);
        popTheBalloon.SetActive(false);

        //play balloon pop animation
        questionBalloons[i].GetComponent<Animator>().SetTrigger("Pop");

        Invoke("QuestionFalling", 0.5f);
    }

    public void QuestionFalling()
    {
        //paper falling
        questionBalloons[i].transform.GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        Invoke("SpawnNextBalloon", 2.5f);
    }

    public void SpawnNextBalloon()
    {
        i++;
        questionBalloons[i].SetActive(true);
        PlaySE(fall);
    }

    public void PlaySE(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
