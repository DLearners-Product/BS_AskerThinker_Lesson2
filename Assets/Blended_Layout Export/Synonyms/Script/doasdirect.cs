using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class doasdirect : MonoBehaviour
{
    public GameObject G_Select1, G_Select2;
    public GameObject G_final;
    public bool click;
    public int count, match;

    public AudioSource AS_Wrong, AS_correct;
    // Start is called before the first frame update
    void Start()
    {
        click = true;
        G_final.SetActive(false);
    }

    public void BUT_OnClicking()
    {
        AS_correct.Play();
        if (click)
        {
            count++;
            if (count % 2 == 1)
            {
                G_Select1 = EventSystem.current.currentSelectedGameObject;
                G_Select1.transform.GetChild(0).GetComponent<Image>().enabled = true;
                //G_Select1.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                G_Select1.GetComponent<Button>().enabled = false;
            }
            if (count % 2 == 0)
            {
                G_Select2 = EventSystem.current.currentSelectedGameObject;
                G_Select2.transform.GetChild(0).GetComponent<Image>().enabled = true;
               // G_Select2.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                G_Select2.GetComponent<Button>().enabled = false;
                click = false;
                check();
            }

        }
    }
    public void check()
    {

        if (G_Select1.tag == G_Select2.tag)
        {
            match++; 
            G_Select1.transform.GetChild(0).GetComponent<Image>().enabled = true;
           // G_Select1.transform.GetChild(0).GetComponent<Image>().color = Color.green;
            G_Select2.transform.GetChild(0).GetComponent<Image>().enabled = true;
           // G_Select2.transform.GetChild(0).GetComponent<Image>().color = Color.green;
            
            //correct audio
            
           /* int I_count = int.Parse(G_Select1.tag);
            arrowandimage(I_count-1);*/
            click = true;
        }
        else
        {
            AS_Wrong.Play();
           // G_Select1.transform.GetChild(0).GetComponent<Image>().color = Color.red;
          //  G_Select2.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            Invoke("wrong", 1f);
            //wrong audio
        }
        
        if (match == 6)
        {
            click = false;
            Invoke("finalscreen", 2f);
        }
    }
    public void finalscreen()
    {
        G_final.SetActive(true);
    }
    public void wrong()
    {
        G_Select1.transform.GetChild(0).GetComponent<Image>().enabled = false;
        G_Select2.transform.GetChild(0).GetComponent<Image>().enabled = false;
        G_Select1.GetComponent<Button>().enabled = true;
        G_Select2.GetComponent<Button>().enabled = true;
        click = true;
    }
    public void arrowandimage(int index)
    {
    }
}