using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuessingActivity : MonoBehaviour
{
    //audio
    [SerializeField] private AudioSource voSource;
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioClip fall;
    [SerializeField] private AudioClip balloonPop;
    [SerializeField] private AudioClip whoosh;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip blackScreenClip;

    //text
    [SerializeField] private TextMeshProUGUI[] guessAnswers;

    //inputfield
    [SerializeField] private TMP_InputField[] guessInputField;

    //button
    [SerializeField] private Button btnSubmit;

    //GO
    [SerializeField] private GameObject saved;
    [SerializeField] private GameObject popTheBalloon;
    [SerializeField] private GameObject whiteScreen;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject[] questionBalloons;

    private int i = 0;
    private Dictionary<int, string> childsGuessAnswers;

    private void Start()
    {
        StartCheckingForChildInput();

        childsGuessAnswers = new Dictionary<int, string>();
    }

    private void StartCheckingForChildInput()
    {
        guessInputField[i].onValueChanged.AddListener(delegate { OnGuessEnteredCheck(); });
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
        PlayVO(buttonClick);
        saved.SetActive(true);

        //show answer stored confirmation to child
        Invoke("Save", 0.5f);
    }

    public void Save()
    {
        guessInputField[i].GetComponent<Animator>().SetTrigger("Disappear");
        PlayVO(whoosh);
        btnSubmit.gameObject.SetActive(false);
        guessInputField[i].text = "";
        whiteScreen.SetActive(false);

        Invoke("ShowPopTheBalloon", 1.5f);
    }

    public void ShowPopTheBalloon()
    {
        //ask child to pop the balloon
        popTheBalloon.SetActive(true);
        saved.SetActive(false);
    }

    public void OnClickBalloon()
    {
        //balloon pop
        PlaySE(balloonPop);

        //play balloon pop animation
        questionBalloons[i].GetComponent<Animator>().SetTrigger("Pop" + (i + 1));

        Invoke("QuestionFalling", 0.5f);
    }

    public void QuestionFalling()
    {
        //paper falling
        questionBalloons[i].transform.GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        popTheBalloon.SetActive(false);
        PlaySE(fall);
        Invoke("SpawnNextBalloon", 2.5f);
    }

    public void SpawnNextBalloon()
    {
        i++;

        if (i == questionBalloons.Length)
        {
            Invoke("ShowBlackScreen", 1f);
        }


        questionBalloons[i].SetActive(true);
        whiteScreen.SetActive(true);
        StartCheckingForChildInput();
        questionBalloons[i - 1].SetActive(false);
    }

    public void PlaySE(AudioClip clip)
    {
        seSource.clip = clip;
        seSource.Play();
    }

    public void PlayVO(AudioClip clip)
    {
        voSource.clip = clip;
        voSource.Play();
    }

    public void ShowBlackScreen()
    {
        PlaySE(blackScreenClip);
        blackScreen.SetActive(true);
    }
}