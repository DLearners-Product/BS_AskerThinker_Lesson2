using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Yesorno : MonoBehaviour
{
    public GameObject[] GA_Questions;
    public int I_Qcount;
    public Text TXT_Current, TXT_Max;
    public GameObject G_Final;
    bool B_CanClick;
    public AudioSource AS_Correct, AS_Wrong;
    public GameObject G_Instruction;
    public AudioSource AS_Empty;
    public AudioClip[] ACA_Clips;
    // Start is called before the first frame update
    void Start()
    {
        G_Final.SetActive(false);
        I_Qcount = 0;
        for (int i = 0; i < GA_Questions.Length; i++)
        {
            GA_Questions[i].SetActive(false);
           
        }
        if(G_Instruction.GetComponent<AudioSource>().clip != null)
        {
            Invoke(nameof(THI_ShowQuestion), G_Instruction.GetComponent<AudioSource>().clip.length);
        }
        else
        {
            THI_ShowQuestion();
        }
       
        TXT_Max.text = GA_Questions.Length.ToString();
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
    }

    public void BUT_Speaker()
    {
        AS_Empty.clip = ACA_Clips[I_Qcount];
        AS_Empty.Play();
    }
    void THI_ShowQuestion()
    {
        AS_Empty.Stop();
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();

        for (int i = 0; i < GA_Questions.Length; i++)
        {
            GA_Questions[i].SetActive(false);
            
        }
        GA_Questions[I_Qcount].SetActive(true);
        B_CanClick = true;

    }
    public void BUT_Next()
    {
        if (I_Qcount < GA_Questions.Length - 1)
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

    public void BUT_Clicking()
    {
        if (B_CanClick)
        {
            GameObject G_Selected = EventSystem.current.currentSelectedGameObject;
            if (G_Selected.tag == "answer")
            {
                AS_Correct.Play();
                G_Selected.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                B_CanClick = false;
            }
            else
            {
                AS_Wrong.Play();
                G_Selected.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }

        }
    }
}
