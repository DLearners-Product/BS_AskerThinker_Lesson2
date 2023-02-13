using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;

public class ChildQuestionHandler : MonoBehaviour
{
    //const
    private string demoQ = "what made the balloon to float?";

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
    [SerializeField] private TextMeshProUGUI scrollText;

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
    [SerializeField] private Transform questionBalloonsParent;
    [SerializeField] private GameObject[] questionBalloons;
    [SerializeField] private GameObject popTheBalloon;
    [SerializeField] private List<string> q;
    [SerializeField] private List<string> qTag;
    [SerializeField] private Dictionary<int, string> childQuestions;
    [SerializeField] private GameObject detailedScrollArea;
    [SerializeField] private GameObject[] helpQuestionBalloons;

    [Header("GAME DATA")]
    public List<string> STRL_childData;
    public string STR_Data;

    private int MIN_QUESTION_COUNT;
    private int MAX_QUESTION_COUNT;
    private int questionNo;
    private int clipCount;
    private int balloonCount;
    private int helpBalloonCount;
    private GameObject instantiatedBalloon;
    private int childChoice;
    private int qNo;

    private void Awake()
    {
        balloonCount = 0;
        helpBalloonCount = 0;
        clipCount = 0;
        childChoice = 0;
        qNo = 0;
    }

    void Start()
    {
        childQuestions = new Dictionary<int, string>();
        questionNo = 1;
        MIN_QUESTION_COUNT = 2;
        MAX_QUESTION_COUNT = 4;
        bharatAnim.SetBool("Think", true);
        q = new List<string>();

        nameInputField.onValueChanged.AddListener(delegate { OnNameEnteredCheck(); });
        StartCheckingForChildInput();
        NewBalloon();
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
        if (clipCount == clips.Length) clipCount = 0;   //resetting the audio clipCount back to '0' if audio clip list end is reached

        //play sound and animation
        PlaySE(clips[clipCount]);
        clipCount++;

        bharatAnim.SetTrigger("Jump");

        /*        if (questionNo == MAX_QUESTION_COUNT)
                {
                    enterButton.interactable = false;
                }*/

        string question = enteredQuestion.text;

        //adding to dictionary
        childQuestions.Add(questionNo, question);

        //adding question to list
        q.Add(question);
        //setting tag
        SetQuestionTag(question);


        //adding to player prefs
        string questionNumber = questionNo.ToString();
        PlayerPrefs.SetString(questionNumber, question);
        PlayerPrefs.SetInt("qCount", questionNo);
        //cleaing text after entering
        questionInputField.text = "";



        //incrementing question number
        questionNo++;
        qNo++;
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
            /* THI_TrackChildData();

             StartCoroutine(IN_SendDataToDB());

             slotChildQuestions.SetActive(true);

             //play accepted sound
             audioSource.clip = buttonClick;
             audioSource.Play();*/
        }
    }

    public void AnalyzeQuestion()
    {
        List<string> qCategory = new List<string>();
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

        missedQCategory.text = questionCategories;

        popup.SetActive(true);
        childNameQuestionWeb.text = PlayerPrefs.GetString("childName");
    }

    public void OnClickBackButton()
    {
        popup.SetActive(false);
    }

    public void SetQuestionTag(string question)
    {

        if (question.Contains("why not") || question.Contains("Why not"))
        {
            qTag.Add("why not");
        }
        else if (question.Contains("why") || question.Contains("Why"))
        {
            qTag.Add("why");
        }
        else if (question.Contains("what if") || question.Contains("What if"))
        {
            qTag.Add("what if");
        }
        else if (question.Contains("what") || question.Contains("What"))
        {
            qTag.Add("what");
        }
        else if (question.Contains("who") || question.Contains("Who"))
        {
            qTag.Add("who");
        }
        else if (question.Contains("where") || question.Contains("Where"))
        {
            qTag.Add("where");
        }
        else if (question.Contains("when") || question.Contains("When"))
        {
            qTag.Add("when");
        }
        else if (question.Contains("how") || question.Contains("How"))
        {
            qTag.Add("how");
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

            PlaySE(buttonClick);
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
        for (int i = 0; i < q.Count; i++)
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
        PlaySE(buttonClick);
        helpWindow.SetActive(true);
    }

    public void OnClickBalloon()
    {
        //balloon pop
        PlaySE(balloonPop);

        //balloon pop animation
        if (helpWindow.activeInHierarchy)
        {
            //help window balloon pop animation
            helpQuestionBalloons[helpBalloonCount].GetComponent<Animator>().SetTrigger("Pop");
        }
        else
        {
            //enter child question window balloon pop animation
            instantiatedBalloon.GetComponent<Animator>().SetTrigger("Pop" + (balloonCount + 1));
        }

        Invoke("QuestionFalling", 0.5f);
    }

    public void QuestionFalling()
    {
        //
        if (helpWindow.activeInHierarchy)
        {
            //help window paper falling
            helpQuestionBalloons[helpBalloonCount].transform.GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Invoke("OnHelpWindowBoxFalling", 2f);
        }
        else
        {
            //enter child question window paper falling
            instantiatedBalloon.transform.GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Invoke("SpawnNextBalloon", 2.5f);
        }

        popTheBalloon.SetActive(false);
        PlaySE(fall);

    }

    public void SpawnNextBalloon()
    {
        //destroying current balloon
        if (instantiatedBalloon != null) Destroy(instantiatedBalloon);

        //destroying helpQuestionBalloon


        balloonCount++;

        if (balloonCount == questionBalloons.Length)
        {
            balloonCount = 0;
        }

        //instantiating next balloon
        NewBalloon();
    }

    public void NewBalloon()
    {
        instantiatedBalloon = Instantiate(questionBalloons[balloonCount], questionBalloonsParent);
    }

    public void OnClickProceedButton()
    {
        PlaySE(buttonClick);
        questionInputField.text = "";

        if (childChoice == 1)   //FIRST BALLOON
        {
            //type a demo question

            //goto child question entering area
            helpWindow.SetActive(false);

            //demo question
            //what made the balloon to float?
            StartCoroutine(PrintDemoQuestion());
        }
        else if (childChoice == 2)  //SECOND BALLOON
        {
            //give one or two words, enabler had to pitch in and form a question
            //if this didn't help child, ask whether to start all over again?

            //goto child question entering area
            helpWindow.SetActive(false);
        }
        else if (childChoice == 3)  //THIRD BALLOON
        {
            //enabler had to pitch in and allow child to type

            //goto child question entering area
            helpWindow.SetActive(false);

            //goto child question entering area
            helpWindow.SetActive(false);
        }
        else if (childChoice == 4)  //FOURTH BALLOON
        {
            //enabler had to pitch in and allow child to type

            //goto child question entering area
            helpWindow.SetActive(false);
        }
        else if (childChoice == 5)  //FIFTH BALLOON
        {
            //enabler had to pitch in and allow child to type

            //goto child question entering area
            helpWindow.SetActive(false);
        }

        detailedScrollArea.SetActive(false);
    }

    IEnumerator PrintDemoQuestion()
    {
        helpWindow.SetActive(false);
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < demoQ.Length; i++)
        {
            questionInputField.text += demoQ[i];
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void OnHelpWindowBalloonPop(int balloonName)
    {
        childChoice = balloonName;
        helpBalloonCount = balloonName - 1;
        OnClickBalloon();
    }

    public void OnHelpWindowBoxFalling()
    {
        detailedScrollArea.SetActive(true);
    }
}