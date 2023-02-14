using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IMoveHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject mainParent;

    private RectTransform m_RectTransform;
    public static Vector3 draggedItemInitialPos;
    public static Vector3 draggedItemCategoryPos;

    void Awake()
    {
        canvas = Main_Blended.OBJ_main_blended.gameObject.GetComponent<Canvas>();
        m_RectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();


    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.50f;
        canvasGroup.blocksRaycasts = false;

        draggedItemInitialPos = transform.position;
        draggedItemCategoryPos = transform.GetChild(1).position;

        //set new parent as this gameobject is dragged out of panel
        mainParent = ChildQuestions.OBJ_ChildQuestions.gameObject;
        transform.SetParent(mainParent.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        Debug.Log(gameObject.name);
        Debug.Log("eventdata : " + eventData);
        Debug.Log("rect transform : " + m_RectTransform);
        Debug.Log("canvas : " + canvas, gameObject);
        m_RectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
    }

    public void OnMove(AxisEventData eventData)
    {
        Debug.Log(eventData.selectedObject.gameObject.name);
    }
}










