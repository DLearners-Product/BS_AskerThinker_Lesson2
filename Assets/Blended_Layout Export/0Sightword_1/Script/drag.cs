using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class drag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector2 mousePos;
    public Vector2 initalPos;

    bool isdrag;
    GameObject otherGameObject;
    
    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        //initalPos = this.GetComponent<RectTransform>().position;
        initalPos = this.transform.position;
    }


    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = mousePos;
        //Debug.Log("Drag");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
       // Debug.Log("End Drag" + otherGameObject.name);
        if(otherGameObject!=null)
        {
            if (this.gameObject.name == otherGameObject.name)
            {
                dragmain.OBJ_dragmain.THI_Correct();
                this.transform.position = otherGameObject.transform.position;
               // this.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
                this.GetComponent<drag>().enabled = false;
            }
            else
            {
               // Nextonly.OBJ_Nextonly.THI_Wrong();
               // dragmain.OBJ_dragmain.THI_wrg();
                this.transform.position = initalPos;
            }
        }
        else
        {
            this.transform.position = initalPos;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Drop")
            otherGameObject = other.gameObject;
        Debug.Log(otherGameObject.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Drop")
            otherGameObject = null;

    }

}
