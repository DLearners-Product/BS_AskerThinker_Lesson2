using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingActivity : MonoBehaviour
{
    public const float RESOLUTION = 0.1f;
    //line
    [SerializeField] private Camera _camera;
    [SerializeField] private Line _linePrefab;
    private Line _currentLine;


    private void Update()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);


        if (Input.GetMouseButton(0))
            _currentLine.SetPosition(mousePos);


    }
}