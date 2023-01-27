using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HoverAudio : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
  /*  public AudioSource audioSource;
    public AudioClip clip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1.1f, 1.1f, 0);
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1, 1, 0);
        audioSource.Stop();
   }
  */

    public Image STR_Image;
    public Sprite SPR;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<Moneygame>().B_CanClick)
        {
            STR_Image.gameObject.SetActive(true);
            STR_Image.transform.GetChild(0).GetComponent<Image>().sprite = SPR;
            STR_Image.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
        }
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        STR_Image.gameObject.SetActive(false);
    }
}
