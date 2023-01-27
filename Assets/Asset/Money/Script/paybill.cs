using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class paybill : MonoBehaviour
{
    public GameObject[] GA_Questions;

    public GameObject[] GA_Bill;
    public TMP_InputField[] IFA_Amount;
    public string[] STRA_amount;
    public int I_Qcount;
    public Text TXT_Current, TXT_Max;
    public GameObject G_Final;
    bool B_CanClick;
    public AudioSource AS_Correct, AS_Wrong;
    public GameObject G_Instruction;
    public GameObject G_next, G_back,G_submit;
    // Start is called before the first frame update
    void Start()
    {
        G_Final.SetActive(false);
        G_next.SetActive(false);
        G_back.SetActive(false);
        G_submit.SetActive(false);
        I_Qcount = 0;
        for (int i = 0; i < GA_Questions.Length; i++)
        {
            GA_Questions[i].SetActive(false);
            GA_Bill[i].SetActive(false);
        }
        Invoke(nameof(THI_ShowQuestion), G_Instruction.GetComponent<AudioSource>().clip.length);
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
        TXT_Max.text = GA_Questions.Length.ToString();
    }

    void THI_ShowQuestion()
    {
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
        G_next.SetActive(true);
        G_back.SetActive(true);
        G_submit.SetActive(true);
        for (int i = 0; i < GA_Questions.Length; i++)
        {
            GA_Questions[i].SetActive(false);
           // GA_Bill[i].SetActive(false);
        }
        GA_Questions[I_Qcount].SetActive(true);
        GA_Bill[I_Qcount].SetActive(true);
        if (I_Qcount == 5)
        {
            B_CanClick = true;
        }
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
            for (int i = 0; i < IFA_Amount.Length; i++)
            {
                if (IFA_Amount[i].text == STRA_amount[i].ToString())
                {
                    IFA_Amount[i].GetComponent<Image>().color = Color.green;
                   
                }
                else
                {
                    AS_Wrong.Play();
                    IFA_Amount[i].GetComponent<Image>().color = Color.red;
                    B_CanClick = true;
                }
            }
           // THI_Check();
        }
    }

   

}
