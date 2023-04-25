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

    public void SaveChildData()
    {
        ChildsData childsData = new ChildsData();

        childsData.question = questionBalloons[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        childsData.category = "guess";
        childsData.answer = guessInputField[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;
        childsData.map = "O_L";

        //converting string to JSON
        //and saving it to STRL_DATA
        //and joining it to STR_DATA
        string toJson = JsonUtility.ToJson(childsData);
        //Debug.Log("json : " + toJson);
        Main_Blended.OBJ_main_blended.STRL_DATA.Add(toJson);
        //Debug.Log("strl data : " + Main_Blended.OBJ_main_blended.STRL_DATA);
        Main_Blended.OBJ_main_blended.STR_DATA = string.Join(",", Main_Blended.OBJ_main_blended.STRL_DATA);
        Debug.Log("str data : " + Main_Blended.OBJ_main_blended.STR_DATA);
    }

    public void OnClickSubmitButton()
    {
        SaveChildData();

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




    /*
        public void THI_TrackChildData()
        {
            ChildsData childsData = new ChildsData();
            //childData.child_name = childName.text;
            //childData.question = childQuestion.text;
            childsData.question = guessInputField[i].text;


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
    */



    /*
        public IEnumerator IN_SendDataToDB()
        {
            WWWForm form = new WWWForm();
            form.AddField("slide_id", "8");
            form.AddField("user_id", Main_Blended.OBJ_main_blended.userID);
            form.AddField("child_id", Main_Blended.OBJ_main_blended.childID);
            form.AddField("answer_data", STR_Data);

            Debug.Log("Child ID : " + Main_Blended.OBJ_main_blended.childID);
            Debug.Log("User ID : " + Main_Blended.OBJ_main_blended.userID);

            //Debug.Log("string_data : " + STR_Data);
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
    */



}