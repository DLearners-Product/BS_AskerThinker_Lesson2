using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Draw : MonoBehaviour
{
    public static Draw OBJ_draw;
    public Camera m_camera;
   // public GameObject board;
    public GameObject brush;
    public LineRenderer[] LA_Clones;
    LineRenderer currentLineRenderer;
    GameObject G_selected;
    Vector2 lastPos;
    int I_currentsortingorder;
    public bool draw;
    public void Awake()
    {
        OBJ_draw = this;
        m_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void Start()
    {
       
        I_currentsortingorder = 0;
    }
    private void Update()
    {
       
            Drawing();
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))                         //start drawing
        {
            draw = true;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (hit.collider != null)
            {  
                if (draw)
                {
                    if (hit.collider.name == "write")
                    {
                       // Debug.Log(hit.collider.name);

                        CreateBrush();
                    }
                }
            }
            else
            {
                draw = false;
                currentLineRenderer = null;
            }

        }
        else if (Input.GetKey(KeyCode.Mouse0))                         //continous drawing 
        {

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (hit.collider != null)
            {
                if (draw)
                {
                    if (hit.collider.name == "write")
                    {
                        if (mousePos != lastPos)
                        {
                            AddAPoint(mousePos);
                            lastPos = mousePos;
                        }
                    }
                }
            }
            else
            {
                draw = false;
                currentLineRenderer = null;
            }
        }
        else
        {
            draw = false;
            currentLineRenderer = null;
        }
    }
    public void BUT_UNDO()
    {
        int k = LA_Clones.Length - 1;
        Destroy(LA_Clones[k].gameObject);
    }
    public void BUT_Whiteerase()
    {
        brush.GetComponent<LineRenderer>().SetColors(Color.white, Color.white);
        brush.GetComponent<LineRenderer>().SetWidth(0.45f, 0.45f);
      
        Main_Blended.OBJ_main_blended.G_Pointer.transform.GetChild(0).gameObject.SetActive(true);
        Main_Blended.OBJ_main_blended.G_Pointer.transform.GetChild(1).gameObject.SetActive(false);
       
    }

   
    public void BUT_erase()
    {
        for (int i = 0; i < LA_Clones.Length; i++)
        {
            if (LA_Clones[i] != null)
            {
                Destroy(LA_Clones[i].gameObject);
            }
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush,this.transform);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        LA_Clones = GameObject.FindObjectsOfType<LineRenderer>();
        I_currentsortingorder++;
        currentLineRenderer.sortingOrder = I_currentsortingorder;
        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        //Debug.Log("addpoint");

        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }
}