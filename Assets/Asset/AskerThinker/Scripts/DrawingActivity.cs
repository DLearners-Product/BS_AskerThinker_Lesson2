using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingActivity : MonoBehaviour
{
    public Camera camera;
    public GameObject brush;

    LineRenderer currentLineRenderer;
    Vector2 lastPos;
    Vector3 fPos;
    Vector3 lPos;
    Vector2[] arrLineRendererPositions;

    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paintApply;

    //texture
    [SerializeField] private Texture2D[] brushTexture;

    //color
    [SerializeField] private Color[] colorList;

    private int currentToolIndex;
    private int colorCount;
    private Texture2D currentTool;


    private void Start()
    {
        currentToolIndex = 0;
    }


    private void Update()
    {
        Draw();
    }

    void Draw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos != lastPos)
            {
                AddAPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GenerateMesh();
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    public void OnClickTool(int i)
    {
        currentTool = brushTexture[i];

        if (i == 0)
        {
            //pen


        }
        else if (i == 1)
        {
            //color bucket


        }
        else if (i == 2)
        {
            //delete


        }

        //setting default violet color brush
        //brushTexture[8] is the default black color paint bucket cursor
        Cursor.SetCursor(currentTool, new Vector2(brushTexture[0].width / 12, brushTexture[0].height), CursorMode.ForceSoftware);
    }

    void OnDestroy()
    {
        ResetCursor();
    }

    public void ResetCursor()
    {
        //resetting the cursor from brush to normal
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void GenerateMesh()
    {
        Debug.Log(currentLineRenderer.positionCount);

        arrLineRendererPositions = new Vector2[currentLineRenderer.positionCount];

        //creating gameobject with components polygon collider, mesh filter, mesh renderer and making it child of line renderer
        GameObject go = MakePolygonCollider2D.Create(arrLineRendererPositions);
        go.transform.parent = currentLineRenderer.gameObject.transform;

    }

    
}