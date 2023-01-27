using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phrasesentence : MonoBehaviour
{
    public GameObject[] GA_Words;
    public int I_Qcount,I_level;
    public GameObject G_Mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        THI_Mainmenu();
        
    }
    public void THI_Mainmenu()
    {
        for (int i = 0; i < GA_Words.Length; i++)
        {
            GA_Words[i].SetActive(false);
        }
        G_Mainmenu.SetActive(true);
        I_Qcount = 0;
    }
    public void BUT_Next()
    {
        if(I_Qcount<1)
        {
            GA_Words[I_level].transform.GetChild(I_Qcount).gameObject.SetActive(false);
            I_Qcount++;
            GA_Words[I_level].transform.GetChild(I_Qcount).gameObject.SetActive(true);
        }
        else
        {
            THI_Mainmenu();
        }
        
    }
    // Update is called once per frame
    public void BUT_Selecting(int index)
    {
        I_level = index;
        G_Mainmenu.SetActive(false);
        for (int i=0;i< GA_Words.Length;i++)
        {
            GA_Words[i].SetActive(false);
        }
        GA_Words[I_level].SetActive(true);
        GA_Words[I_level].transform.GetChild(0).gameObject.SetActive(true);
        GA_Words[I_level].transform.GetChild(1).gameObject.SetActive(false);
    }
}
