using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Moneygame : MonoBehaviour
{
    public Text TXT_Current, TXT_Max;
    public GameObject G_Character;
    public TextMeshProUGUI TXT_Amount;
    public Sprite[] SPR_Character;
    public string[] STR_String;
    public string[] STR_Answer;
    public Sprite[] SPR_Amount;
    int I_Qcount,I_count;
    public GameObject G_table;
    public TextMeshProUGUI TXT_AmountDisplay;
    public int I_Amount;
    int I_index;
    public GameObject G_Final;
    public AudioSource AS_Correct, AS_Wrong;
    public GameObject G_Wrong;
    public GameObject G_Instruction;
    public GameObject G_Next, G_Back,G_Submit;
    public bool B_CanClick;
    // Start is called before the first frame update
    void Start()
    {
        G_Next.SetActive(false);
        G_Back.SetActive(false);
        G_Submit.SetActive(false);
        G_Character.SetActive(false);
        I_Qcount = 0;
        TXT_Max.text = STR_Answer.Length.ToString();
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
        // THI_ShowQuestion();
        G_Final.SetActive(false);
        Invoke(nameof(THI_ShowQuestion), G_Instruction.GetComponent<AudioSource>().clip.length);
    }

   public void THI_ShowQuestion()
    {
        G_Next.SetActive(false);
        G_Back.SetActive(false);
        
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
        G_Wrong.SetActive(false);
        int I_rand = Random.Range(0, SPR_Character.Length);
        G_Character.GetComponent<Image>().sprite = SPR_Character[I_rand];
        TXT_Amount.text = STR_String[I_Qcount];
        G_Character.SetActive(true);
        for (int i=0;i<G_table.transform.childCount;i++)
        {
            G_table.transform.GetChild(i).gameObject.SetActive(false);
        }
        I_Amount = 0;
        TXT_AmountDisplay.text = I_Amount.ToString();
        I_count = 0;
        B_CanClick = true;
        G_Submit.SetActive(true);
    }

    public void BUT_Clicking()
    {
      
            GameObject G_Selected = EventSystem.current.currentSelectedGameObject;
            
            for (int i = 0; i < SPR_Amount.Length; i++)
            {
                if (G_Selected.name == SPR_Amount[i].name)
                {
                    I_index = i;
                }
            }

            G_table.transform.GetChild(I_count).GetComponent<Image>().sprite = SPR_Amount[I_index];
            G_table.transform.GetChild(I_count).GetComponent<Image>().preserveAspect = true;
            if (int.Parse(G_Selected.name) < 6)
            {
                G_table.transform.GetChild(I_count).transform.localScale = new Vector3(0.5f, 0.5f, 0);
            }
            else
            {
                G_table.transform.GetChild(I_count).transform.localScale = new Vector3(1.5f, 1.5f, 0);
            }
            G_table.transform.GetChild(I_count).gameObject.SetActive(true);
            I_Amount = I_Amount + int.Parse(G_Selected.name);
            TXT_AmountDisplay.text = I_Amount.ToString();
            I_count++;


            if (I_Amount > int.Parse(STR_Answer[I_Qcount]) || I_count > 13)
            {
                G_Wrong.SetActive(true);
                //try again effect
                Invoke(nameof(Reset), 5f);
            }
        
        
    }


    void Reset()
    {
        THI_ShowQuestion();
    }
    public void BUT_Next()
    {
        if(I_Qcount <STR_Answer.Length-1)
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

    public void BUT_Submit()
    {
        if (B_CanClick)
        {
            if (I_Amount == int.Parse(STR_Answer[I_Qcount]))
            {
                B_CanClick = false;
                G_Next.SetActive(true);
                G_Back.SetActive(true);
                AS_Correct.Play();
            }
            else
            {
                AS_Wrong.Play();
            }
        }
    }
}
