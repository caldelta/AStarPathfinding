using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public GameObject GameObject { get; set; }

    public Vector3 Position
    {
        get
        {
            return GameObject.transform.position;
        }
        set
        {
            GameObject.transform.position = value;
        }
    }
}
