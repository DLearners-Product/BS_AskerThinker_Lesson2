using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingActivity : MonoBehaviour
{
    public Camera camera;
    public GameObject brush;

    LineRenderer currentLineRenderer;
    Vector2 lastPos;


    //audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paintApply;

    //texture
    [SerializeField] private Texture2D[] brushTexture;

    //color
    [SerializeField] private Color[] colorList;

    private Color curColor;
    private int colorCount;


    private void Start()
    {
        //setting default violet color brush
        //brushTexture[8] is the default black color paint bucket cursor
        Cursor.SetCursor(brushTexture[3], new Vector2(brushTexture[0].width / 8, brushTexture[0].height), CursorMode.ForceSoftware);

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

}