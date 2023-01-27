using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro : MonoBehaviour
{
    public GameObject[] GA_Questions;
    public int I_Qcount;
    public Text TXT_Current, TXT_Max;
    public GameObject G_Final;
   // public AudioSource AS_Empty;
   // public AudioClip[] ACA_Clips;
    // Start is called before the first frame update
    void Start()
    {
        G_Final.SetActive(false);
        I_Qcount = 0;
        THI_ShowQuestion();
        TXT_Max.text = GA_Questions.Length.ToString();
    }

    void THI_ShowQuestion()
    {
       // AS_Empty.Stop();
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();

        for (int i=0;i<GA_Questions.Length;i++)
        {
            GA_Questions[i].SetActive(false);
        }
        GA_Questions[I_Qcount].SetActive(true);
    }
   /* public void BUT_Speaker()
    {
        AS_Empty.clip = ACA_Clips[I_Qcount];
        AS_Empty.Play();
    }*/
   public void BUT_Next()
    {
        if(I_Qcount<GA_Questions.Length-1)
        {
            I_Qcount++;
            THI_ShowQuestion();
        }
        else
        {
            G_Final.SetActive(true);
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
            G_Final.SetActive(true);
        }
    }
}
