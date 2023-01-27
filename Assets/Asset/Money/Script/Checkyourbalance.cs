using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Checkyourbalance : MonoBehaviour
{
    public Sprite[] SPR_Purchased, SPR_Amountpaid;
    public string[] STR_Price;
    public string[] STR_Paid;
    public int I_Qcount;
    public GameObject G_Final;
    public Text TXT_Current, TXT_Max;
    public GameObject G_Purchased, G_AmountPaid;
    public GameObject[] GA_Balance;
    public GameObject[] GA_Options;
    // public TextMeshProUGUI TMP_Balance;
    public AudioSource AS_Correct, AS_Wrong;
    public GameObject G_Instruction;
    public GameObject G_next, G_back;
    bool B_CanClick;
    void Start()
    {
        I_Qcount = 0;
        THI_OffALL();
        Invoke(nameof(THI_ShowQuestion), G_Instruction.GetComponent<AudioSource>().clip.length);
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
        TXT_Max.text = SPR_Purchased.Length.ToString();
       
    }

    void THI_OffALL()
    {
        G_Final.SetActive(false);
        G_next.SetActive(false);
        G_back.SetActive(false);
        
        G_Purchased.SetActive(false);
        G_AmountPaid.SetActive(false);
        for (int i = 0; i < GA_Options.Length; i++)
        {
            GA_Options[i].SetActive(false);
        }
        for (int i = 0; i < GA_Balance.Length; i++)
        {
            GA_Balance[i].SetActive(false);
        }
    }

    void OnNavigation()
    {
        G_next.SetActive(true);
        G_back.SetActive(true);
    }
    void THI_ShowQuestion()
    {
        THI_OffALL();
        if (I_Qcount < SPR_Purchased.Length)
        {
            G_Purchased.transform.GetChild(1).GetComponent<Image>().sprite = SPR_Purchased[I_Qcount];
            G_Purchased.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
            G_Purchased.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = STR_Price[I_Qcount];
            G_Purchased.SetActive(true);
            Invoke(nameof(THI_Delay1), 2f);

            /* G_next.SetActive(true);
             G_back.SetActive(true);*/
        }

        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
    }
    void THI_Delay1()
    {
        G_AmountPaid.transform.GetChild(1).GetComponent<Image>().sprite = SPR_Amountpaid[I_Qcount];
        G_AmountPaid.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
        G_AmountPaid.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = STR_Paid[I_Qcount];
        G_AmountPaid.SetActive(true);
        Invoke(nameof(THI_Delay2), 2f);
    }
    void THI_Delay2()
    {
        for (int i = 0; i < GA_Balance.Length; i++)
        {
            GA_Balance[i].SetActive(false);
        }
        GA_Balance[I_Qcount].SetActive(true);
        for(int i=0;i<GA_Options.Length;i++)
        {
            GA_Options[i].SetActive(true);
            GA_Options[i].transform.GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        B_CanClick = true;
    }

    public void BUT_Clicking()
    {
        if(B_CanClick)
        {
            GameObject G_Selected = EventSystem.current.currentSelectedGameObject;

            if (I_Qcount == 0 || I_Qcount == 2 || I_Qcount == 4)
            {
                if (G_Selected.name == "Correct")
                {
                    B_CanClick = false;
                    OnNavigation();
                    G_Selected.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                    AS_Correct.Play();
                }
                else
                {
                    G_Selected.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    AS_Wrong.Play();
                }

            }
            else
            {
                if (G_Selected.name == "Wrong")
                {
                    B_CanClick = false;
                    OnNavigation();
                    G_Selected.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                    AS_Correct.Play();
                }
                else
                {
                    G_Selected.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    AS_Wrong.Play();
                }
            }
        }
       
    }
    public void BUT_Next()
    {
        if (I_Qcount < SPR_Purchased.Length - 1)
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
