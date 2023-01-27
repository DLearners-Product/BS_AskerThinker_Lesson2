using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Selectword : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI TEX_tmp;
    public string[] STRA_wordsBefore, STRA_wordsAfter;
    public string STR_Selected;
    public GameObject G_Selected;
    public GameObject[] GA_Questions;
    public int I_Count;
    public AudioSource AS_Empty;
    public AudioClip[] AC_Clips;
    // Start is called before the first frame update
    void Start()
    {
        THI_seperateTMP();
        I_Count = 0;
        for (int i = 0; i < GA_Questions.Length; i++)
        {
            GA_Questions[i].SetActive(false);
        }
        GA_Questions[I_Count].SetActive(true);
    }

    public void PlayAudio(int i)
    {

    }

    void THI_seperateTMP()
    {
        STRA_wordsBefore = new string[0];

        while (TEX_tmp.text.Contains("<"))
        {
            int startIndex = TEX_tmp.text.IndexOf("<");
            int stringLength = TEX_tmp.text.Length - 1;
            string unwantedString = TEX_tmp.text.Substring(startIndex, stringLength - startIndex);
            int endIndex = unwantedString.IndexOf(">") + 1;
            unwantedString = TEX_tmp.text.Substring(startIndex, endIndex);
            TEX_tmp.text = TEX_tmp.text.Replace(unwantedString.Trim(), "");
           // Debug.Log("CLICK TEXT REDEFINED before : " + TEX_tmp.text);
        }

        STRA_wordsBefore = TEX_tmp.text.ToString().Split(' ');
        STRA_wordsAfter = new string[0];


        STRA_wordsAfter = TEX_tmp.text.ToString().Split(' ');
       // Main_Blended.OBJ_main_blended.STRA_beforeWords = STRA_wordsBefore;


        for (int i = 0; i < STRA_wordsAfter.Length; i++)
        {
            STRA_wordsAfter[i] = "<link =" + STRA_wordsAfter[i] + ">" + STRA_wordsAfter[i] + "</link>";
        }
        TEX_tmp.text = string.Join(" ", STRA_wordsAfter);

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        STR_Selected= TEX_tmp.textInfo.linkInfo[TMP_TextUtilities.FindIntersectingLink(TEX_tmp, Input.mousePosition, Camera.main)].GetLinkText();
        for (int i = 0; i < STRA_wordsAfter.Length; i++)
        {
          //  STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + ">" + STRA_wordsBefore[i] + "</link>";
           
            if (STRA_wordsBefore[i] == STR_Selected)
            {
                    if (STR_Selected == "knight") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "Folks") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "knock") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "castle") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "knit") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "write") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "hours") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "design") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "wrote") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "gnomes.") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "wrapper") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
                    if (STR_Selected == "honour") { STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><mark=#ffff0044><u>" + STRA_wordsBefore[i] + "</mark></u></link>"; }
            }
           
        }
        TEX_tmp.text = string.Join(" ", STRA_wordsAfter);
        //Debug.Log(TEX_tmp.text);
    }

    public void BUT_SelectColor()
    {
        G_Selected = EventSystem.current.currentSelectedGameObject;
       
        G_Selected.GetComponent<Outline>().enabled = true;


    }

    public void BUT_Next()
    { 
        if(I_Count<GA_Questions.Length-1)
        {
            I_Count++;
        }
        
        for(int i=0;i< GA_Questions.Length;i++)
        {
            GA_Questions[i].SetActive(false);
        }
        GA_Questions[I_Count].SetActive(true);
    }

    public void BUT_Back()
    {
        if (I_Count >0)
        {
            I_Count--;
        }
            
        for (int i = 0; i < GA_Questions.Length; i++)
        {
            GA_Questions[i].SetActive(false);
        }
        GA_Questions[I_Count].SetActive(true);
    }
}
