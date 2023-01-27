using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Identify : MonoBehaviour
{
    public GameObject[] GA_Question;
    public AudioSource AS_Empty, AS_Correct, AS_Wrong;
    public AudioClip[] AC_Clips;
    public int I_Qcount;
    GameObject G_Selected;
    bool B_CanClick;
    public GameObject G_final;
    public Text TXT_Max, TXT_Current;
    // Start is called before the first frame update
    void Start()
    {
        I_Qcount = 0;
        B_CanClick = true;
        G_final.SetActive(false);
        TXT_Max.text = GA_Question.Length.ToString();
        int i = I_Qcount + 1;
        TXT_Current.text = i.ToString();
        THI_ShowQuestion();
    }
    public void THI_ShowQuestion()
    {
        for(int i=0;i<GA_Question.Length;i++)
        {
            GA_Question[i].SetActive(false);
        }
        GA_Question[I_Qcount].SetActive(true);
        B_CanClick = true;
    }
    public void BUT_Next()
    {
        if(I_Qcount<GA_Question.Length-1)
        {
            I_Qcount++;
            THI_ShowQuestion();
            int i = I_Qcount + 1;
            TXT_Current.text = i.ToString();
        }
        else
        {
            G_final.SetActive(true);
        }
    }
    public void BUT_Speaker()
    {
        AS_Empty.clip = AC_Clips[I_Qcount];
        AS_Empty.Play();
    }
    public void BUT_Clicking()
    {
        if(B_CanClick)
        {
            G_Selected = EventSystem.current.currentSelectedGameObject;
            if (G_Selected.tag == "answer")
            {
                AS_Correct.Play();
                for(int i=0;i<G_Selected.transform.parent.childCount;i++)
                {
                    G_Selected.transform.parent.GetChild(i).gameObject.SetActive(false);
                }
                G_Selected.SetActive(true);
            }
            else
            {
                B_CanClick = false;
                AS_Wrong.Play();
                G_Selected.GetComponent<Image>().color = Color.red;
                Invoke("Off", 1f);
            }
        }
    }
    void Off()
    {
        B_CanClick = true;
        G_Selected.GetComponent<Image>().color = Color.white;
    }
}
