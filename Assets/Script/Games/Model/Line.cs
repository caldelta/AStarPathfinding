using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public LineRenderer LineRenderer { get; set; }
    public Vector3 WorldPos
    {
        get
        {
            return LineRenderer.transform.position;
        }
        set
        {
            LineRenderer.transform.position = value;
        }
    }

    public void SetPositions(Vector3[] array)
    {
        LineRenderer.positionCount = array.Length;
        LineRenderer.SetPositions(array);
    }
}
