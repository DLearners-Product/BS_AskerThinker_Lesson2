using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class texthightlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Text>().color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Text>().color = Color.black;
    }
}
