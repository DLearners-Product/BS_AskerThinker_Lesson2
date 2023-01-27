using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuessingActivity : MonoBehaviour
{
    //audio


    //text
    [SerializeField] private TextMeshProUGUI[] guessAnswers;

    //inputfield
    [SerializeField] private TMP_InputField[] guessInputField;

    //button
    [SerializeField] private Button btnSubmit;

    //GO
    [SerializeField] private GameObject[] questionBalloons;

    private int i = 0;

    private void Start()
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
        //store the answer, show answer stored confirmation to child, ask child to pop the balloon



        i++;
    }



}
