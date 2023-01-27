using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Proyecto26;
using Random = UnityEngine.Random;
using UnityEngine.Video;
using TMPro;
using FrostweepGames.Plugins.GoogleCloud.TextToSpeech;
using System.Linq;
using System.Globalization;
using FrostweepGames.Plugins.Core;
using UnityEngine.Networking;
using SimpleJSON;

public class Main_Blended : MonoBehaviour
{
    public GameObject[] GA_levelsIG;
    public int levelno;
    //public int[] IA_videoSlides;
    public string GameName;

    public GameObject G_Selected;
   // public GameObject welcome, mainmenu, details;

    public static Main_Blended OBJ_main_blended;

    public AudioSource AS_BGM;
    int i_vol;

    public string STR_date_with_time;
    public string sessionStartTime;
    public string sessionEndTime;


    public VideoPlayer Videoplayerinlevel;
    bool B_pause;



    public GameObject G_currenlevel;


    // public GameObject Rating;

    //Worksheet
    public Sprite[] SPR_worksheet;
    public GameObject G_Sprite;
    int index, I_worksheetindex;
    int I_dummy;
    public GameObject G_worksheet;


    //Writing and drawing
    public GameObject G_write;
    public InputField IF_typing;
    public int I_drawcount, I_boardcount;
    public Sprite[] SPR_grid4lines;
    public GameObject G_Pointer;
    public Button[] BUT_BoardButtons;
    public Sprite[]  SPR_Default;

    // emerson -- project blue
    public string[] enablerComments;
    public int[][] slideRatingArray;
    public int[] ratingItemsArray;

    public int MAX_SLIDES;

    public string[] SLIDE_NAMES;
    public string[] TEACHER_INSTRUCTION;
    public bool[] HAS_VIDEO;
    public bool[] HAS_WORKSHEET;

    // emerson -- project blue

    //Reader
    GCTextToSpeech _gcTextToSpeech;
    Voice _currentVoice;
    Voice[] _voices;
    CultureInfo _provider;
    string content;
    AudioSource audioSource;
    public GameObject[] ImmersiveObjects;
    public Image BG;
    public int VoiceNumber;
    public double VoicePitch, VoiceSpeed;
    public string dictWord;
    public GameObject G_ruler, G_focus;
    //int  I_Ruler=0,I_Hand;
    public GameObject G_Zoomrect, G_hand;
    public Sprite[] SPR_Hands;


    public Font[] FA_Fonts;
    public TMP_FontAsset[] TMPFA_Fonts;
    public Color[] CA_Colors, CA_BGColors;

    //WordDB Grammer
    public string[] STRA_beforeWords;
   // public TextMeshProUGUI TEX_postDBconfirmation;
    public string STR_clickedWord;
   // public GameObject[] GA_grammarPostButtons;
    public bool B_Reset;
    public List<string> LS_WORDS;
   // public Toggle TOG_noun, TOG_verb, TOG_adjective, TOG_adverb;
    //public bool B_noun, B_verb, B_adjective, B_adverb;
    public bool B_highlight;
    public string HEXA_highlightDB, HEXA_onClick, HEXA_default;
   // int I_Noun = 0, I_Adverb = 0, I_Adjective = 0, I_Verb = 0;
    public string STR_Passage;
    public string STR_API;
    public bool B_Reader;

    void Awake()
    {
        /*SLIDE_NAMES = new string[MAX_SLIDES];
        TEACHER_INSTRUCTION = new string[MAX_SLIDES];
        HAS_VIDEO = new bool[MAX_SLIDES];
        HAS_WORKSHEET = new bool[MAX_SLIDES];*/


        PlayerPrefs.DeleteAll();
        AS_BGM.volume = 0.5f;

        Time.timeScale = 1;
       // GameName = "Crash Game";

        OBJ_main_blended = this;

       // mainmenu.SetActive(true); // live

       /////////////////////////////////////////////////////////// STR_date_with_time = System.DateTime.Now.ToString("dd-MM-yy HH:mm");

        i_vol = 0;
        B_pause = false;
        levelno = 0;
        THI_cloneLevels();


    }
    private void Start()
    {
        LS_WORDS = new List<string>();
       
        G_worksheet.transform.GetChild(0).gameObject.SetActive(false);
        G_Pointer.SetActive(false);
        G_write.SetActive(false);
        IF_typing.gameObject.SetActive(false);
        I_drawcount = 0;
        I_boardcount = 0;


        Application.ExternalEval("OnAppReady()");


        enablerComments = new string[MAX_SLIDES];
        slideRatingArray = new int[MAX_SLIDES][];
        ratingItemsArray = new int[2];

        //Reading
        _gcTextToSpeech = GCTextToSpeech.Instance;
        audioSource = _gcTextToSpeech.gameObject.GetComponent<AudioSource>();
        _provider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        _provider.NumberFormat.NumberDecimalSeparator = ".";
        _gcTextToSpeech.GetVoicesSuccessEvent += _gcTextToSpeech_GetVoicesSuccessEvent;
        _gcTextToSpeech.SynthesizeSuccessEvent += _gcTextToSpeech_SynthesizeSuccessEvent;
        _gcTextToSpeech.SynthesizeFailedEvent += _gcTextToSpeech_SynthesizeFailedEvent;

        //WordDB Grammer
        STR_clickedWord = "";
        B_highlight = false;
      //  for (int i = 0; i < GA_grammarPostButtons.Length; i++)
      //  {
      //      GA_grammarPostButtons[i].GetComponent<Button>().interactable = false;
      //  }
    }
    private void Update()
    {
        if (G_ruler.activeInHierarchy)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = G_ruler.transform.position.z - Camera.main.transform.position.z;
            G_ruler.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }

        if (G_focus.activeInHierarchy)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = G_focus.transform.position.z - Camera.main.transform.position.z;
            G_focus.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
       // G_hand

        if (G_hand.activeInHierarchy)
        {
            Cursor.visible = false;
            Vector3 pos = Input.mousePosition;
            pos.z = G_hand.transform.position.z - Camera.main.transform.position.z;
            G_hand.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
        else
        {
            if (G_Pointer.activeInHierarchy == false)
            Cursor.visible = true;
        }

        if(G_Pointer.activeInHierarchy)
        {
            Cursor.visible = false;
            Vector3 pos = Input.mousePosition;
            pos.z = G_hand.transform.position.z - Camera.main.transform.position.z;
            G_Pointer.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
        else
        {
            if (G_hand.activeInHierarchy == false)
                Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Escape clicked");
            if (G_ruler.activeInHierarchy)
                G_ruler.SetActive(false);

            if (G_focus.activeInHierarchy)
                G_focus.SetActive(false);

            if(G_Zoomrect.activeInHierarchy)
            {
                //Debug.Log("zoom off");
                BG.gameObject.transform.localScale = new Vector2(1, 1);
                G_Zoomrect.SetActive(false);
            }
        }
        //WordDB Grammer
        if (STR_clickedWord != "")
        {
            //  for (int i = 0; i < GA_grammarPostButtons.Length; i++)
            // {
            //     GA_grammarPostButtons[i].GetComponent<Button>().interactable = true;
            // }
           // STR_clickedWord = "<color=" + HEXA_onClick + "><u>" + STR_clickedWord + "</u></color>";
        }

    }
    //WordDB Grammer
    public void BUT_postGrammerToDB()
    {
      //  TEX_postDBconfirmation.text = "<color=" + HEXA_onClick + "><u>" + STR_clickedWord + "</u></color> has been added as a " + EventSystem.current.currentSelectedGameObject.name + " to DL database";
      //  STR_clickedWord = "";
      //  for (int i = 0; i < GA_grammarPostButtons.Length; i++)
      //  {
      //      GA_grammarPostButtons[i].GetComponent<Button>().interactable = false;
      //  }
      //  B_Reset = true;
      //  Invoke("THI_confirmationTextDefault", 2f);
    }

    public void TOG_Noun()
    {
        B_Reset = true;
        BUT_Grammar();
        StartCoroutine(SendAPI("noun", "#FF66FF"));
        B_highlight = true;
    }
    public void TOG_Verb()
    {
        B_Reset = true;
        BUT_Grammar();
        StartCoroutine(SendAPI("verb", "#F3451B"));
        B_highlight = true;

    }
    public void TOG_adjectives()
    {
        B_Reset = true;
        BUT_Grammar();
        StartCoroutine(SendAPI("adjective", "#05AB14"));
        B_highlight = true;

    }
    public void TOG_adverbs()
    {
        B_Reset = true;
        BUT_Grammar();
        StartCoroutine(SendAPI("adverb", "#D7A222"));
        B_highlight = true;

    }

    public void TOG_sight_words()
    {
        B_Reset = true;
        BUT_Grammar();
        StartCoroutine(SendAPI("sight_words", "#96ADFC"));
        B_highlight = true;
    }

    public void TOG_syllabification()
    {
        B_Reset = true;
        BUT_Grammar();
        StartCoroutine(SendAPI("syllabification", "#000000"));
        B_highlight = true;
    }
    public void TOG_preposition()
    {
        B_Reset = true;
        BUT_Grammar();
        StartCoroutine(SendAPI("preposition", "#9D512B"));
        B_highlight = true;
    }

    public void TOG_article()
    {
        B_Reset = true;
        BUT_Grammar();
        StartCoroutine(SendAPI("article", "#E047A3"));
        B_highlight = true;
    }



    //Reader
    public void SynthesizeButtonOnClickHandler()
    {
        if(B_Reader)
        {
            GetVoicesButtonOnClickHandler();
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Text>() != null)
            {
                content = EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text;
                Debug.Log("TEXT : " + content);
            }
            if (EventSystem.current.currentSelectedGameObject.GetComponent<TextMeshProUGUI>() != null)
            {
                content = EventSystem.current.currentSelectedGameObject.GetComponent<TextMeshProUGUI>().text;
                Debug.Log("TMP ORIGINAL: " + content);
                while (content.Contains("<"))
                {
                    int startIndex = content.IndexOf("<");
                    int stringLength = content.Length - 1;
                    string unwantedString = content.Substring(startIndex, stringLength - startIndex);
                    int endIndex = unwantedString.IndexOf(">") + 1;
                    unwantedString = content.Substring(startIndex, endIndex);
                    content = content.Replace(unwantedString.Trim(), "");
                    Debug.Log("TMP REDEFINED : " + content);
                }
            }

            if (string.IsNullOrEmpty(content) || _currentVoice == null)
                return;

            _gcTextToSpeech.Synthesize(content, new VoiceConfig()
            {
                gender = _currentVoice.ssmlGender,
                languageCode = _currentVoice.languageCodes[0],
                name = _currentVoice.name
            }, false, VoicePitch, VoiceSpeed, _currentVoice.naturalSampleRateHertz);
        }
    }


    void _gcTextToSpeech_SynthesizeSuccessEvent(PostSynthesizeResponse response, long requestId)
    {
        audioSource.clip = _gcTextToSpeech.GetAudioClipFromBase64(response.audioContent, Constants.DEFAULT_AUDIO_ENCODING);
        audioSource.Play();
    }


    void _gcTextToSpeech_SynthesizeFailedEvent(string error, long requestId)
    {
        Debug.Log(error);
    }

    void _gcTextToSpeech_GetVoicesSuccessEvent(GetVoicesResponse response, long requestId)
    {
        _voices = response.voices;
        _currentVoice = _voices[VoiceNumber];
    }
    void GetVoicesButtonOnClickHandler()
    {
        _gcTextToSpeech.GetVoices(new GetVoicesRequest()
        {
            languageCode = _gcTextToSpeech.PrepareLanguage((Enumerators.LanguageCode)11)
        });
    }

    IEnumerator SendAPI(string grammar,string colorcode)
    {
        yield return new WaitForSeconds(1f);
        WWWForm form = new WWWForm();
        form.AddField("passage", STR_Passage);

       // Debug.Log("Sending :" + STR_Passage);

        UnityWebRequest www = UnityWebRequest.Post(STR_API, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("passage : " + www.error);  // error
        }
        else
        {
            Debug.Log("Response : " + www.downloadHandler.text);

            JSONNode n = JSON.Parse(www.downloadHandler.text);

            LS_WORDS.Clear();
           //LS_WORDS = new
            for (int i = 0; i < n[grammar].Count; i++)
            {
                Debug.Log(n[grammar][i]);
                LS_WORDS.Add(n[grammar][i]);
            }
            for (int i = 0; i < ImmersiveObjects.Length; i++)
            {
                if (ImmersiveObjects[i] != null)
                {
                    if (ImmersiveObjects[i].activeInHierarchy)
                    {
                        if (ImmersiveObjects[i].GetComponent<clickText>() != null)
                        {
                            ImmersiveObjects[i].GetComponent<clickText>().THI_highlightWordsAfterRetrieve(colorcode);
                        }
                    }
                }
            }
        }
    }

    public void THI_Separatewords(string content)
    {
        

      //  LS_noun.Clear();
      //  for(int i=0;i<n["noun"].Count;i++)
       // {
       //     Debug.Log(n["noun"][i]);
      //  }
        /*LS_noun.Clear();
          LS_adjective.Clear();
          LS_sightwords.Clear();
          LS_verb.Clear();
          LS_adverb.Clear();



          for(int k=0;k<n.Count;k++)
          {
              Type Genericlist = typeof(List<string>);

              if (n[k].Count > 0)
              {
                  for (int i = 0; i < n[k].Count; i++)
                  {

                      LS_noun[i] = n[k][i];
                  }
              }
          }


          if (n["adjective"].Count > 0)
          {
              for (int i = 0; i < n["adjective"].Count; i++)
              {
                  Main_Blended.OBJ_main_blended.LS_adjective[i] = n["adjective"][i];
              }
          }*/

        //LS_noun=
        /* for (int i = 0; i < jSONNode.Count; i++)
         {
             //Debug.Log(jSONNode[i][0].ToString().Replace('"',' ').Trim());
             for (int j = 0; j < jSONNode[i].Count; j++)
             {
                 switch (i)
                 {
                     case 0:
                         LS_noun.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                         break;
                     case 1:
                         LS_adjective.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                         break;
                     case 2:
                         LS_sightwords.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                         break;
                     case 3:
                         LS_verb.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                         break;
                     case 4:
                         LS_adverb.Add(jSONNode[i][j].ToString().Replace('"', ' ').Trim());
                         break;
                 }
             }
         }*/
    }
    public void BUT_Reader()
    {
       // B_Reader = true;
        for (int i = 0; i < ImmersiveObjects.Length; i++)
        {
            if (ImmersiveObjects[i].activeInHierarchy)
            {
                if (ImmersiveObjects[i].GetComponent<clickText>() != null)
                {
                    ImmersiveObjects[i].GetComponent<clickText>().enabled = false;
                }
            }
        }
    }
    public void BUT_Grammar()
    {
        for (int i = 0; i < ImmersiveObjects.Length; i++)
        {
            if (ImmersiveObjects[i] != null)
            {
                if(ImmersiveObjects[i].activeInHierarchy)
                {
                    if (ImmersiveObjects[i].GetComponent<TextMeshProUGUI>() != null)
                    {
                        if (ImmersiveObjects[i].GetComponent<clickText>() != null)
                        {
                           // if (B_Reader)
                           // {
                                ImmersiveObjects[i].GetComponent<clickText>().enabled = true;
                                B_Reader = false;
                                //reader off
                                HEXA_default = "#" + ColorUtility.ToHtmlStringRGB(ImmersiveObjects[i].GetComponent<TextMeshProUGUI>().color);
                           // }
                            /*else
                            {
                                B_Reader = true;
                                ImmersiveObjects[i].GetComponent<clickText>().enabled = false;
                            }*/
                        }
                    }
                }
               /* else
                {
                    if (ImmersiveObjects[i].GetComponent<TextMeshProUGUI>() != null)
                    {
                        if (ImmersiveObjects[i].GetComponent<clickText>() != null)
                        {
                            ImmersiveObjects[i].GetComponent<clickText>().enabled = false;
                          
                        }
                    }
                }*/
            }
        }
    }
    

    public void BUT_Hand(int index)
    {
        if (index == 0)
        {
            G_hand.SetActive(false);
        }
        else
        {
            G_hand.SetActive(true);
            G_hand.GetComponent<Image>().sprite = SPR_Hands[index-1];
        }

    }
    public void BUT_Ruler(int index)
    {
        if(index==0)
        {
            G_ruler.SetActive(false);
        }
        else
        {
            G_ruler.SetActive(true);
        }
    }
    public void BUT_Focus(int I_Focusindex)
    {
        if(I_Focusindex==0)
        {
            G_focus.SetActive(false);
        }
        else
        {
            G_focus.SetActive(true);
            for (int i = 0; i < G_focus.transform.childCount; i++)
            {
                G_focus.transform.GetChild(i).gameObject.SetActive(false);
            }
            G_focus.transform.GetChild(I_Focusindex-1).gameObject.SetActive(true);
        }
    }
    public void BUT_FontcolorPick(int I_Color)
    {
        for (int i = 0; i < ImmersiveObjects.Length; i++)
        {
            if (ImmersiveObjects[i] != null)
            {
                if (ImmersiveObjects[i].GetComponent<TextMeshProUGUI>() != null)
                { 
                    ImmersiveObjects[i].GetComponent<TextMeshProUGUI>().color = CA_Colors[I_Color];
                }
                else if (ImmersiveObjects[i].GetComponent<Text>() != null)
                {
                    ImmersiveObjects[i].GetComponent<Text>().color = CA_Colors[I_Color];
                }
            }
        }
    }

    public void BUT_BGcolorPick(int I_Color)
    {
        if (BG != null)
        {
            BG.color = CA_BGColors[I_Color];
        }
    }

    public void BUT_fontPick(int I_Font)
    {
        for (int i = 0; i < ImmersiveObjects.Length; i++)
        {
            if (ImmersiveObjects[i] != null)
            {
                if (ImmersiveObjects[i].GetComponent<TextMeshProUGUI>() != null)
                {
                    ImmersiveObjects[i].GetComponent<TextMeshProUGUI>().font = TMPFA_Fonts[I_Font];
                }
                else if(ImmersiveObjects[i].GetComponent<Text>() != null)
                {
                    ImmersiveObjects[i].GetComponent<Text>().font = FA_Fonts[I_Font];
                }
                   
            }
        }
      
    }
    public void BUT_zoomPick(float scale)
    {
        if(scale==0)
        {
            G_Zoomrect.SetActive(false);
            BG.gameObject.transform.localScale = new Vector2(1, 1);
            BG.gameObject.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            G_Zoomrect.SetActive(true);
            BG.gameObject.transform.localScale = new Vector2(scale, scale);
            G_Zoomrect.GetComponent<ScrollRect>().content = BG.gameObject.GetComponent<RectTransform>();
        }
    }
    public void BUT_backWorksheet()
    {
        if (I_worksheetindex > 0)
        {
            I_worksheetindex--;
            G_Sprite.GetComponent<Image>().sprite = SPR_worksheet[I_worksheetindex];
        }

    }
    public void BUT_nextWorksheet()
    {
        if (I_worksheetindex < SPR_worksheet.Length - 1)
        {
            I_worksheetindex++;
            G_Sprite.GetComponent<Image>().sprite = SPR_worksheet[I_worksheetindex];
        }
    }
    public void BUT_Worksheeton()
    {
        index = levelno;
        if (index == 8) { I_worksheetindex = 0; }
       // if (index == 9) { I_worksheetindex = 1; }
        I_dummy++;
        if (I_dummy % 2 != 0)
        {
            G_worksheet.transform.GetChild(0).gameObject.SetActive(true);
            G_Sprite.GetComponent<Image>().sprite = SPR_worksheet[I_worksheetindex];
        }
        else
        {
            G_worksheet.transform.GetChild(0).gameObject.SetActive(false);
        }
    }


    public void BUT_draw()
    {
       
        I_drawcount++;
        if (I_drawcount % 2 != 0)
        {
            if(G_currenlevel.GetComponent<Draw>()!=null)
            {
                G_currenlevel.GetComponent<Draw>().enabled = false;
            }
            G_write.SetActive(true);
            Draw.OBJ_draw.brush.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            Draw.OBJ_draw.brush.GetComponent<LineRenderer>().SetWidth(0.05f, 0.05f);
            Draw.OBJ_draw.BUT_erase();
            G_Pointer.SetActive(true);
            G_Pointer.transform.GetChild(0).gameObject.SetActive(false);
            G_Pointer.transform.GetChild(1).gameObject.SetActive(true);
            for (int i=0;i<G_write.transform.childCount;i++)
            {
                G_write.transform.GetChild(i).gameObject.SetActive(false);
            }
            IF_typing.gameObject.SetActive(false);
            G_write.GetComponent<Image>().color = new Color32(255, 255, 255, 1);
            I_boardcount = 0;
        }
        else
        {
            if (G_currenlevel.GetComponent<Draw>() != null)
            {
                G_currenlevel.GetComponent<Draw>().enabled = true;
            }
            G_write.SetActive(false);
            G_Pointer.SetActive(false);
            I_drawcount = 0;
            Draw.OBJ_draw.BUT_erase();
        }
    }
    public void THI_Changesprite(Sprite SPR)
    {
        for (int i= 0;i<BUT_BoardButtons.Length;i++)
        {
            BUT_BoardButtons[i].GetComponent<Image>().sprite = SPR_Default[i];
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = SPR;
    }
    public void BUT_grid_board(int index)
    {
        Draw.OBJ_draw.brush.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
        Draw.OBJ_draw.brush.GetComponent<LineRenderer>().SetWidth(0.05f, 0.05f);
       
        G_Pointer.SetActive(true);
        G_Pointer.transform.GetChild(0).gameObject.SetActive(false);
        G_Pointer.transform.GetChild(1).gameObject.SetActive(true);

        G_write.GetComponent<Image>().sprite = SPR_grid4lines[0];

        IF_typing.gameObject.SetActive(false);
        IF_typing.text = "";
        G_write.transform.GetChild(4).GetComponent<Button>().interactable = true;
       /* if (index == 0)
        {
            for (int i = 0; i < Draw.OBJ_draw.LA_Clones.Length; i++)
            {
                if (Draw.OBJ_draw.LA_Clones[i].startColor == Color.white)
                {
                    Draw.OBJ_draw.LA_Clones[i].sortingOrder = 2;
                }
            }
        }*/
        if (index!=0)
        {
            G_write.transform.GetChild(4).GetComponent<Button>().interactable = false;
            Draw.OBJ_draw.BUT_erase();
            if (index == 3)
            {
                IF_typing.gameObject.SetActive(true);
            }
            else
            {
                G_write.GetComponent<Image>().sprite = SPR_grid4lines[index];
            }
        }
    }
    public void BUT_Board()
    {
        if (G_currenlevel.GetComponent<Draw>() != null)
        {
            G_currenlevel.GetComponent<Draw>().enabled = false;
        }
        for (int i = 0; i < G_write.transform.childCount; i++)
        {
            G_write.transform.GetChild(i).gameObject.SetActive(true);
        }
        G_write.transform.GetChild(4).GetComponent<Button>().interactable = true;
        I_boardcount++;
        if (I_boardcount % 2 != 0)
        {
            G_write.SetActive(true);
            BUT_grid_board(0);
            G_write.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            I_drawcount = 0;
        }
        else
        {
            if (G_currenlevel.GetComponent<Draw>() != null)
            {
                G_currenlevel.GetComponent<Draw>().enabled = true;
            }
            G_write.SetActive(false);
            G_Pointer.SetActive(false);
            IF_typing.gameObject.SetActive(false);
            IF_typing.text = "";
            I_boardcount = 0;
            Draw.OBJ_draw.BUT_erase();
        }
    }

    public void THI_cloneLevels()
    {
        B_pause = false;
        ImmersiveObjects = new GameObject[0];
        G_worksheet.transform.GetChild(0).gameObject.SetActive(false);
        //  Debug.Log("zzzzzzz111111==="+ImmersiveObjects.Length);
        if (G_currenlevel != null)
        {
            Destroy(G_currenlevel);
        }
        var currentLevel = Instantiate(GA_levelsIG[levelno]);
        currentLevel.transform.SetParent(GameObject.Find("Game_Panel").transform, false);
        currentLevel.transform.SetAsFirstSibling();
        G_currenlevel = currentLevel;
        // Debug.Log("zzzzzzz222222 :" + ImmersiveObjects.Length);
        //NEW IMMERSIVE READING
        if (currentLevel.GetComponent<Image>() != null)
        {
            BG = currentLevel.GetComponent<Image>();
        }

        ImmersiveObjects = GameObject.FindGameObjectsWithTag("Immersive");
        // Debug.Log("zzzzzzz4444 :" + ImmersiveObjects.Length);
        for (int i = 0; i < ImmersiveObjects.Length; i++)
        {
            if (ImmersiveObjects[i] != null)
            {
                if (ImmersiveObjects[i].GetComponent<Button>() != null)
                {
                    ImmersiveObjects[i].GetComponent<Button>().onClick.AddListener(SynthesizeButtonOnClickHandler);
                }
            }
        }
        BUT_Reader();
        Invoke("FindImmersive", 0.5f);

    }
    public void FindImmersive()
    {
        STR_Passage = "";

        if (ImmersiveObjects != null)
        {
            // Debug.Log("clone Immersive");
            for (int k = 0; k < ImmersiveObjects.Length; k++)
            {
                if (ImmersiveObjects[k] != null)
                {
                    if (ImmersiveObjects[k].GetComponent<TextMeshProUGUI>() != null)
                    {
                        if (ImmersiveObjects[k].GetComponent<clickText>() != null)
                        {
                            STR_Passage += ImmersiveObjects[k].GetComponent<TextMeshProUGUI>().text;
                        }

                    }
                    if (ImmersiveObjects[k].GetComponent<Text>() != null)
                    {
                        if (ImmersiveObjects[k].GetComponent<clickText>() != null)
                        {
                            STR_Passage += ImmersiveObjects[k].GetComponent<Text>().text;
                        }
                    }
                }

            }
        }
    }


    public void BUT_reset()
    {
        THI_cloneLevels();
    }

    public void THI_videoSlidesPausePlay()
    {
        Videoplayerinlevel = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        if (B_pause)
        {
            if (Videoplayerinlevel != null)
            {
                // Debug.Log("Play video");
                Videoplayerinlevel.Play();
                B_pause = false;
            }
        }
        else
        {
            // Debug.Log("Pause Bool false");
            if (Videoplayerinlevel != null)
            {
                //   Debug.Log("Pause Video");
                Videoplayerinlevel.Pause();
                B_pause = true;
            }
        }
    }
  
    public void BUT_pause()
    {
        THI_videoSlidesPausePlay();
    }


    public void levelselect(int level)
    {
        B_pause = false;
        levelno = level;
      //  THI_videoSlidesMute();
        THI_cloneLevels();
    }
    


    public void BUT_volume()
    {
        i_vol++;
        if (i_vol % 2 == 1)
        {
            AS_BGM.volume = 0;
            PlayerPrefs.SetInt("Mute", 1);
        }
        if (i_vol % 2 == 0)
        {
            AS_BGM.volume = 0.5f;
            PlayerPrefs.SetInt("Mute", 0);
        }
    }

    public void setCurrentDate(string date)
    {
        STR_date_with_time = date;
    }
    public void setSessionStartTime(string starttime)
    {
        sessionStartTime = starttime;
    }
    public void setSessionEndTime(string endtime)
    {
        sessionEndTime = endtime;
    }

    public void HTML_next()
    {
        if (levelno < GA_levelsIG.Length - 1)
        {
            levelno++;
            THI_cloneLevels();
        }
    }

    public void HTML_Back()
    {
        if (levelno > 0)
        {
            levelno--;
            THI_cloneLevels();
        }
    }

    public void HTML_Exit()
    {
        levelno = MAX_SLIDES - 1;
        THI_cloneLevels();
    }
    public void HTML_enablerComments(string comments)
    {
        //GameName = STRA_levelsIG[levelno];
        enablerComments[levelno] = comments;

        Debug.Log(comments);
        //consoleMsg.GetComponent<TextMeshProUGUI>().text = comments;

        HTML_enablerCommentsDB();

    }
    public void HTML_enablerCommentsDB()
    {
        EnablerCmtsDB comments = new EnablerCmtsDB();
        RestClient.Put("https://blended-html-default-rtdb.firebaseio.com/"
            + STR_date_with_time + "/"
            + GameName + "/"
            + GetValues.OBJ_getvalues.enabler_name + "/"
            + GetValues.OBJ_getvalues.parent_mobile + GetValues.OBJ_getvalues.parent_name + "/"
            + GetValues.OBJ_getvalues.child_name + "/"
            + "Comments" + "/" + ".json", comments);
    }

    public void PostDataToFirebase()
    {
        storevalues values = new storevalues();
        RestClient.Put("https://blended-html-default-rtdb.firebaseio.com/"
            + STR_date_with_time + "/" 
            + GameName + "/" 
            + GetValues.OBJ_getvalues.enabler_name + "/" 
            + GetValues.OBJ_getvalues.parent_mobile + GetValues.OBJ_getvalues.parent_name + "/" 
            + GetValues.OBJ_getvalues.child_name + "/" 
            + "Details" + ".json", values);
    }

    public void PostRatingToFirebase(string mySlideRating)
    {
       string[] xRating =  mySlideRating.Split(',');

        ratingItemsArray[0] = Int32.Parse(xRating[0]);
        ratingItemsArray[1] = Int32.Parse(xRating[1]);


        slideRatingArray[levelno] = ratingItemsArray;

        if(slideRatingArray[levelno] != null)
        {
            SlideRating rating = new SlideRating(slideRatingArray[levelno]);
            Debug.Log("Rating : " + rating);

            RestClient.Put("https://blended-html-default-rtdb.firebaseio.com/"
                + STR_date_with_time + "/"
                + GameName + "/"
                + GetValues.OBJ_getvalues.enabler_name + "/"
                + GetValues.OBJ_getvalues.parent_mobile + GetValues.OBJ_getvalues.parent_name + "/"
                + GetValues.OBJ_getvalues.child_name + "/"
                + "Rating" + "/"
                + SLIDE_NAMES[levelno]
                + ".json", rating);
        }
    }
}