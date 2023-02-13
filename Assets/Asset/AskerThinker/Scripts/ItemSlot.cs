using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip rightClip;
    [SerializeField] private AudioClip wrongClip;

    //animator
    [SerializeField] private Animator animator;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        //eventData is Q1, Q2, Q3, Q4
        if (eventData.pointerDrag != null)
        {
            if (gameObject.CompareTag(eventData.pointerDrag.gameObject.tag))
            {
                animator.SetTrigger("Active");

                //making it to sit on the slot
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

                //positioning the category
                eventData.pointerDrag.transform.GetChild(1).transform.position = transform.position + new Vector3(0f, -1.1f, 0f);

                //making the category to be visible
                eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(true);

                //populating the category text
                eventData.pointerDrag.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

                //playing correct answer sound effect
                audioSource.clip = rightClip;
                audioSource.Play();
            }
            else
            {
                //taking Q1, Q2, Q3, Q4 back to its initial position
                eventData.pointerDrag.GetComponent<RectTransform>().position = DragAndDrop.draggedItemInitialPos;

                //playing incorrect answer sound effect
                audioSource.clip = wrongClip;
                audioSource.Play();
            }
        }

    }
}