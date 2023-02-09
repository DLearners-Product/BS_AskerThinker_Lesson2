using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ChildQuestionHandler : MonoBehaviour
{
    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip accepted;
    [SerializeField] private AudioClip denied;
    [SerializeField] private AudioClip balloonPop;
    [SerializeField] private AudioClip fall;

    //animator
    [SerializeField] private Animator bharatAnim;

    //text
    [SerializeField] private TextMeshProUGUI enteredQuestion;
    [SerializeField] private TextMeshProUGUI missedQCategory;
    [SerializeField] private TextMeshProUGUI childName;
    [SerializeField] private TextMeshProUGUI childQuestion;
    [SerializeField] private TextMeshProUGUI childNameQuestionWeb;

    //button
    [SerializeField] private Button submitButton;
    [SerializeField] private Button enterButton;

    //input field
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField questionInputField;

    //GO
    [SerializeField] private GameObject getChildName;
    [SerializeField] private GameObject getChildQuestions;
    [SerializeField] private GameObject slotChildQuestions;
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject helpWindow;
    [SerializeField] private GameObject[] questionBalloons;
    [SerializeField] private GameObject popTheBalloon;

    [SerializeField] GameObject[] q;

    [Header("GAME DATA")]
    public List<string> STRL_childData;
    public string STR_Data;

    private int MIN_QUESTION_COUNT;
    private int MAX_QUESTION_COUNT;
    private Dictionary<int, string> childQuestions;
    private int questionNo;
    private int clipCount;
    private int balloonCount;

    void Start()
    {
        balloonCount = 0;
        clipCount = 0;
        childQuestions = new Dictionary<int, string>();
        questionNo = 1;
        MIN_QUESTION_COUNT = 4;
        MAX_QUESTION_COUNT = 4;
        bharatAnim.SetBool("Think", true);

        nameInputField.onValueChanged.AddListener(delegate { OnNameEnteredCheck(); });
        StartCheckingForChildInput();
    }

    private void StartCheckingForChildInput()
    {
        questionInputField.onValueChanged.AddListener(delegate { OnNewQuestionEnteredCheck(); });
    }

    public void OnNameEnteredCheck()
    {
        if (nameInputField.text.Length > 0)
        {
            submitButton.gameObject.SetActive(true);
        }
        else
        {
            submitButton.gameObject.SetActive(false);
        }
    }

    public void OnNewQuestionEnteredCheck()
    {
        if (questionInputField.text.Length > 0)
        {
            enterButton.gameObject.SetActive(true);
        }
        else
        {
            enterButton.gameObject.SetActive(false);
        }
    }

    public void OnClickEnterQuestion()
    {
        if (clipCount == clips.Length) clipCount = 0;   //resetting the clipCount back to '0' if clip end is reached

        //play sound and animation
        PlaySE(clips[clipCount]);
        clipCount++;

        bharatAnim.SetTrigger("Jump");

        if (questionNo == MAX_QUESTION_COUNT)
        {
            enterButton.interactable = false;
        }

        string question = enteredQuestion.text;

        //adding to dictionary
        //childQuestions.Add(questionNo, question);

        //adding to player prefs
        string questionNumber = questionNo.ToString();
        PlayerPrefs.SetString(questionNumber, question);
        PlayerPrefs.SetInt("qCount", questionNo);
        //cleaing text after entering
        questionInputField.text = "";

        //setting tag
        SetQuestionTag(question, questionNo);

        //incrementing question number
        questionNo++;
    }

    public void OnClickNextButton()
    {
        if (questionNo <= MIN_QUESTION_COUNT)
        {
            AnalyzeQuestion();

            //play denied sound
            audioSource.clip = denied;
            audioSource.Play();
        }
        else
        {
            THI_TrackChildData();

            StartCoroutine(IN_SendDataToDB());

            slotChildQuestions.SetActive(true);

            //play accepted sound
            audioSource.clip = buttonClick;
            audioSource.Play();
        }
    }

    public void AnalyzeQuestion()
    {
        /*        List<string> qCategory = new List<string>();
                qCategory.Add("why");
                qCategory.Add("why not");
                qCategory.Add("what");
                qCategory.Add("what if");
                qCategory.Add("who");
                qCategory.Add("where");
                qCategory.Add("when");
                qCategory.Add("how");

                for (int i = 1; i < questionNo; i++)
                {
                    string question = PlayerPrefs.GetString(i.ToString());

                    if (question.Contains("why not") == true)
                    {
                        qCategory.Remove("why not");
                    }
                    else if (question.Contains("why") == true)
                    {
                        qCategory.Remove("why");
                    }
                    else if (question.Contains("what if") == true)
                    {
                        qCategory.Remove("what if");
                    }
                    else if (question.Contains("what") == true)
                    {
                        qCategory.Remove("what");
                    }
                    else if (question.Contains("who") == true)
                    {
                        qCategory.Remove("who");
                    }
                    else if (question.Contains("where") == true)
                    {
                        qCategory.Remove("where");
                    }
                    else if (question.Contains("when") == true)
                    {
                        qCategory.Remove("when");
                    }
                    else if (question.Contains("how") == true)
                    {
                        qCategory.Remove("how");
                    }
                }

                string questionCategories = "";

                for (int i = 0; i < qCategory.Count; i++)
                {
                    if (i == qCategory.Count - 1) questionCategories += qCategory[i];
                    else questionCategories += qCategory[i] + ", ";
                }

                missedQCategory.text = questionCategories;*/

        popup.SetActive(true);
        childNameQuestionWeb.text = PlayerPrefs.GetString("childName");
    }

    public void OnClickBackButton()
    {
        popup.SetActive(false);
    }

    public void SetQuestionTag(string question, int qNo)
    {
        if (question.Contains("why not") || question.Contains("Why not"))
        {
            q[qNo - 1].tag = "why not";
        }
        else if (question.Contains("why") || question.Contains("Why"))
        {
            q[qNo - 1].tag = "why";
        }
        else if (question.Contains("what if") || question.Contains("What if"))
        {
            q[qNo - 1].tag = "what if";
        }
        else if (question.Contains("what") || question.Contains("What"))
        {
            q[qNo - 1].tag = "what";
        }
        else if (question.Contains("who") || question.Contains("Who"))
        {
            q[qNo - 1].tag = "who";
        }
        else if (question.Contains("where") || question.Contains("Where"))
        {
            q[qNo - 1].tag = "where";
        }
        else if (question.Contains("when") || question.Contains("When"))
        {
            q[qNo - 1].tag = "when";
        }
        else if (question.Contains("how") || question.Contains("How"))
        {
            q[qNo - 1].tag = "how";
        }
    }

    public void OnClickSubmitButton()
    {
        if (nameInputField.text == "" || nameInputField.text == null)
        {
            Debug.Log("name cant be empty");
        }
        else
        {
            PlayerPrefs.SetString("childName", nameInputField.text);
            getChildName.SetActive(false);
            getChildQuestions.SetActive(true);
            childName.text = PlayerPrefs.GetString("childName");

            audioSource.clip = buttonClick;
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        Debug.Log("before clearing : " + childQuestions.Count);
        childQuestions.Clear();
    }


    //API CALL
    public void THI_TrackChildData()
    {
        DBmanager childData = new DBmanager();
        childData.child_name = childName.text;
        childData.question = childQuestion.text;

        childData.answers = new List<Answers>();

        int count = 0;
        for (int i = 0; i < q.Length; i++)
        {
            if (PlayerPrefs.HasKey((i + 1) + "")) count++;
        }

        for (int i = 0; i < count; i++)
        {
            childData.answers.Add(new Answers(PlayerPrefs.GetString((i + 1).ToString())));
        }

        string toJson = JsonUtility.ToJson(childData);
        Debug.Log("json : " + toJson);
        STRL_childData.Add(toJson);
        Debug.Log("child data : " + STRL_childData);
        STR_Data = string.Join(",", STRL_childData);
    }

    public IEnumerator IN_SendDataToDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("slide_id", "8");
        form.AddField("answer_data", STR_Data);

        Debug.Log(STR_Data);
        UnityWebRequest www = UnityWebRequest.Post("https://dlearners.in/template_and_games/Game_Generator/api/save_child_blended_data.php", form);

        yield return www.SendWebRequest();
        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log("Sending data to DB failed : " + www.error);
        }
        else
        {
            Debug.Log("Sending data to DB success : " + www.downloadHandler.text);
        }

    }

    public void PlaySE(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnClickHelpButton()
    {
        helpWindow.SetActive(true);
    }

    public void OnClickBalloon()
    {
        //balloon pop
        PlaySE(balloonPop);

        //play balloon pop animation
        questionBalloons[balloonCount].GetComponent<Animator>().SetTrigger("Pop" + (balloonCount + 1));

        Invoke("QuestionFalling", 0.5f);
    }

    public void QuestionFalling()
    {
        //paper falling
        questionBalloons[balloonCount].transform.GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        popTheBalloon.SetActive(false);
        PlaySE(fall);
        Invoke("SpawnNextBalloon", 2.5f);
    }

    public void SpawnNextBalloon()
    {
        balloonCount++;

        if (balloonCount == questionBalloons.Length)
        {
            //Invoke("ShowBlackScreen", 1f);
            balloonCount = 0;
        }

        questionBalloons[balloonCount].SetActive(true);
        StartCheckingForChildInput();
        questionBalloons[balloonCount - 1].SetActive(false);
    }
}