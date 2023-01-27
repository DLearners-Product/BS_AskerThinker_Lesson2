using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class dragmain : MonoBehaviour
{
    public static dragmain OBJ_dragmain;
    public GameObject[] GA_Questions;
    public int I_Qcount,I_Count;
    public GameObject  G_final;
    public AudioSource AS_crt, AS_wrg;
    public Text TXT_Current, TXT_Max;

    public void Start()
    {
        OBJ_dragmain = this;

        I_Qcount = 0;
        TXT_Max.text = GA_Questions.Length.ToString();
        int j = I_Qcount + 1;
        
        G_final.SetActive(false);
        THI_ShowQuestion();
    }
   
    void THI_ShowQuestion()
    {
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
        for (int i=0;i<GA_Questions.Length;i++)
        {
            GA_Questions[i].SetActive(false);
        }
        GA_Questions[I_Qcount].SetActive(true);
    }
    public void THI_Correct()
    {

        AS_crt.Play();
    }
    
    public void THI_wrg()
    {
        AS_wrg.Play();
    }
    
   public void BUT_Next()
    {
        if(I_Qcount<GA_Questions.Length-1)
        {
            I_Qcount++;
            THI_ShowQuestion();
        }
        else
        {
            G_final.SetActive(true);
        }
    }
    public void BUT_Back()
    {
        if (I_Qcount > 0)
        {
            I_Qcount--;
            THI_ShowQuestion();
        }
        else
        {
            G_final.SetActive(true);
        }
    }

   

}
