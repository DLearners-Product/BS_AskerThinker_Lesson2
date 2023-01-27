using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Countpiggy : MonoBehaviour
{
    public GameObject[] GA_Questions;
    public int I_Qcount;
    public Text TXT_Current, TXT_Max;
    public GameObject G_Final;
    bool B_CanClick;
    public AudioSource AS_Correct, AS_Wrong;
    public GameObject G_Instruction;
    public GameObject G_Back, G_Next;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GA_Questions.Length; i++)
        {
            GA_Questions[i].SetActive(false);
        }
        TXT_Max.text = GA_Questions.Length.ToString();
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();
        G_Final.SetActive(false);
        I_Qcount = 0;
        Invoke(nameof(THI_ShowQuestion), G_Instruction.GetComponent<AudioSource>().clip.length);
        G_Back.SetActive(false);
        G_Next.SetActive(false);
    }

    void THI_ShowQuestion()
    {
        if(I_Qcount==0)
        {
            G_Back.SetActive(true);
            G_Next.SetActive(true);
        }
        
        // TXT_Max.text = GA_Questions.Length.ToString();
        int j = I_Qcount + 1;
        TXT_Current.text = j.ToString();

        for (int i = 0; i < GA_Questions.Length; i++)
        {
            GA_Questions[i].SetActive(false);
        }
       
        GA_Questions[I_Qcount].transform.GetChild(0).gameObject.SetActive(false);
        GA_Questions[I_Qcount].transform.GetChild(2).gameObject.SetActive(false);
        GA_Questions[I_Qcount].SetActive(true);
        B_CanClick = true;
        Invoke(nameof(opencoin), 1f);
    }

    void opencoin()
    {
        GA_Questions[I_Qcount].transform.GetChild(0).gameObject.SetActive(true);
        GA_Questions[I_Qcount].transform.GetChild(2).gameObject.SetActive(true);
    }
    public void BUT_Clicking()
    {
        if(B_CanClick)
        {
            GameObject G_Selected = EventSystem.current.currentSelectedGameObject;
            if(G_Selected.tag=="answer")
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
}
