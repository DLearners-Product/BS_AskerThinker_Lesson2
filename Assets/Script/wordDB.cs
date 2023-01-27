using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class wordDB : MonoBehaviour
{

    public static wordDB OBJ_wordDB;
    public string[] STRA_beforeWords;
    public TextMeshProUGUI TEX_postDBconfirmation;
    public string STR_clickedWord;
    public GameObject[] GA_grammarPostButtons;
    public bool B_Reset;
    public string[] STRA_noun, STRA_verb, STRA_adjective, STRA_adverb;
    public Toggle TOG_noun, TOG_verb, TOG_adjective, TOG_adverb;
    public bool B_noun, B_verb, B_adjective, B_adverb;
    public bool B_highlight;
    public string HEXA_highlightDB,HEXA_onClick, HEXA_default;

    private void Awake()
    {
        OBJ_wordDB = this;
    }
    void Start()
    {
        STR_clickedWord = "";
        B_highlight = false;
        for (int i = 0; i < GA_grammarPostButtons.Length; i++)
        {
            GA_grammarPostButtons[i].GetComponent<Button>().interactable = false;
        }
    }
    void Update()
    {
        if (STR_clickedWord != "")
        {
            for (int i = 0; i < GA_grammarPostButtons.Length; i++)
            {
                GA_grammarPostButtons[i].GetComponent<Button>().interactable = true;
            }
            TEX_postDBconfirmation.text = "<color="+ HEXA_onClick + "><u>" + STR_clickedWord + "</u></color>" + " belongs to which grammar group?";
        }
    }

    public void BUT_postGrammerToDB()
    {
        TEX_postDBconfirmation.text = "<color="+HEXA_onClick+"><u>" + STR_clickedWord + "</u></color> has been added as a " + EventSystem.current.currentSelectedGameObject.name + " to DL database";
        STR_clickedWord = "";
        for (int i = 0; i < GA_grammarPostButtons.Length; i++)
        {
            GA_grammarPostButtons[i].GetComponent<Button>().interactable = false;
        }
        B_Reset = true;
        Invoke("THI_confirmationTextDefault", 2f);
    }

    void THI_confirmationTextDefault()
    {
        TEX_postDBconfirmation.text = "Select a word!";
    }

    void THI_generateGrammarWordsFromDB()
    {
        // API TO RETRIEVE GRAMMAR WORDS
    }

    public void TOG_Noun()
    {
        if (TOG_noun.isOn)
        {
            B_noun = true;
            B_verb = false;
            B_adjective = false;
            B_adverb = false;
            B_highlight = true;
            TOG_adjective.isOn = false;
            TOG_adverb.isOn = false;
            TOG_verb.isOn = false;
        }
        else
        {
            B_noun = false;
        }
    }
    public void TOG_Verb()
    {
        if (TOG_verb.isOn)
        {
            B_verb = true;
            B_noun = false;
            B_adjective = false;
            B_adverb = false;
            B_highlight = true;
            TOG_adjective.isOn = false;
            TOG_adverb.isOn = false;
            TOG_noun.isOn = false;
        }
        else
        {
            B_verb = false;
        }
    }
    public void TOG_adjectives()
    {
        if (TOG_adjective.isOn)
        {
            B_adjective = true;
            B_noun = false;
            B_verb = false;
            B_adverb = false;
            B_highlight = true;
            TOG_noun.isOn = false;
            TOG_adverb.isOn = false;
            TOG_verb.isOn = false;
        }
        else
        {
            B_adjective = false;
        }
    }
    public void TOG_adverbs()
    {
        if (TOG_adverb.isOn)
        {
            B_adverb = true;
            B_noun = false;
            B_verb = false;
            B_adjective = false;
            B_highlight = true;
            TOG_noun.isOn = false;
            TOG_adjective.isOn = false;
            TOG_verb.isOn = false;
        }
        else
        {
            B_adverb = false;
        }
    }
}


