using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class clickText : MonoBehaviour, IPointerClickHandler
{
    public static clickText OBJ_Clicktext;
    public TextMeshProUGUI TEX_tmp;
    public string[] STRA_wordsBefore, STRA_wordsAfter;
    public List<int> IL_IndexList;

    
    void Start()
    {
        OBJ_Clicktext = this;
        TEX_tmp = GetComponent<TextMeshProUGUI>();
       // Main_Blended.OBJ_main_blended.STR_Passage = TEX_tmp.text;
        //Debug.Log("Click Text : " + Main_Blended.OBJ_main_blended.STR_Passage);
        THI_seperateTMP();
       
    }
    void Update()
    {
        if (Main_Blended.OBJ_main_blended.B_Reset)
        {
            for (int i = 0; i < STRA_wordsAfter.Length; i++)
            {
                STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + ">" + STRA_wordsBefore[i] + "</link>";
            }
            TEX_tmp.text = string.Join(" ", STRA_wordsAfter);
            Main_Blended.OBJ_main_blended.B_Reset = false;
        }
        //THI_highlightWordsAfterRetrieve();
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
            Debug.Log("CLICK TEXT REDEFINED before : " + TEX_tmp.text);
        }

        STRA_wordsBefore = TEX_tmp.text.ToString().Split(' ');
        STRA_wordsAfter = new string[0];

       /*while (TEX_tmp.text.Contains("<"))
        {
            int startIndex = TEX_tmp.text.IndexOf("<");
            int stringLength = TEX_tmp.text.Length - 1;
            string unwantedString = TEX_tmp.text.Substring(startIndex, stringLength - startIndex);
            int endIndex = unwantedString.IndexOf(">") + 1;
            unwantedString = TEX_tmp.text.Substring(startIndex, endIndex);
            TEX_tmp.text = TEX_tmp.text.Replace(unwantedString.Trim(), "");
            Debug.Log("TMP REDEFINED : " + TEX_tmp.text);
        }*/
        STRA_wordsAfter = TEX_tmp.text.ToString().Split(' ');
        Main_Blended.OBJ_main_blended.STRA_beforeWords = STRA_wordsBefore;

        
        for (int i = 0; i < STRA_wordsAfter.Length; i++)
        {
            STRA_wordsAfter[i] = "<link =" + STRA_wordsAfter[i] + ">" + STRA_wordsAfter[i] + "</link>";
        }
        TEX_tmp.text = string.Join(" ", STRA_wordsAfter);
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Main_Blended.OBJ_main_blended.B_Reader)
        {
            Main_Blended.OBJ_main_blended.CancelInvoke();
            Main_Blended.OBJ_main_blended.STR_clickedWord = TEX_tmp.textInfo.linkInfo[TMP_TextUtilities.FindIntersectingLink(TEX_tmp, Input.mousePosition, Camera.main)].GetLinkText();
            for (int i = 0; i < STRA_wordsAfter.Length; i++)
            {
                STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + ">" + STRA_wordsBefore[i] + "</link>";
                if (STRA_wordsBefore[i] == Main_Blended.OBJ_main_blended.STR_clickedWord)
                {
                    Debug.Log("Matching : 2");
                    STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><u><color="+Main_Blended.OBJ_main_blended.HEXA_onClick+">" + STRA_wordsBefore[i] + "</color></u></link>";
                    Debug.Log("Matching : 3" + STRA_wordsAfter[i]);
                }
            }
            TEX_tmp.text = string.Join(" ", STRA_wordsAfter);
            Debug.Log("Matching : joined");
        }
    }


    public void THI_highlightWordsAfterRetrieve(string colorcode)
    {
       
       // Debug.Log("Hightlight 1");
        IL_IndexList = new List<int>();
        
        //back to default

        for (int i = 0; i < STRA_wordsAfter.Length; i++)
        {
            
            STRA_wordsAfter[i] = "<link =" + STRA_wordsBefore[i] + "><color=" + Main_Blended.OBJ_main_blended.HEXA_default + ">" + STRA_wordsBefore[i] + "</color></link>";
          //  Debug.Log("Hightlight 2");
        }
        for (int i = 0; i < STRA_wordsBefore.Length; i++)

        {

            foreach (string s in Main_Blended.OBJ_main_blended.LS_WORDS)

            {

                if (STRA_wordsBefore[i] == s)

                {

                    IL_IndexList.Add(i);

                }

            }

        }
        foreach (int index in IL_IndexList)
        {
            STRA_wordsAfter[index] = "<link =" + STRA_wordsBefore[index] + "><color=" +colorcode+ ">" + STRA_wordsBefore[index] + "</color></u></link>";
           // Debug.Log("Highlight color" + STRA_wordsBefore[index]);
        }
        TEX_tmp.text = string.Join(" ", STRA_wordsAfter);
        Main_Blended.OBJ_main_blended.B_highlight = false;
       

    }

}