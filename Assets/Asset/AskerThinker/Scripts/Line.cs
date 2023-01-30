using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    //ref
    [SerializeField] private LineRenderer _lineRenderer;


    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos))
            return;

        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
    }

    public bool CanAppend(Vector2 pos)
    {
        if (_lineRenderer.positionCount == 0)
            return true;

        return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), pos) > DrawingActivity.RESOLUTION;
    }




}
