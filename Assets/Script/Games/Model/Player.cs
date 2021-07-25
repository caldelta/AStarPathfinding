using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public GameObject GameObject { get; set; }
    private const int kSpeed = 2;

    public bool IsRunning { get; set; }

    public PlayerController Controller
    {
        get
        {
            return GameObject.GetComponent<PlayerController>();
        }
    }

    public Vector3 WorldPos
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

    public Cell CellPos { get; set; }

    public void Run(Vector3[] path)
    {
        Controller.StartCoroutine(DoRun(path));
    }

    IEnumerator DoRun(Vector3[] path)
    {
        float time = 0;
        IsRunning = true;
        foreach (var p in path)
        {
            var dir = (p - WorldPos).normalized;
            var dis = dir.magnitude;
            while(dis > 0.1f)
            {
                WorldPos += dir * kSpeed * Time.deltaTime;
                dis = (p - WorldPos).magnitude;
                yield return null;
            }
            Debug.Log("next waypoint");
            time = 0;                        
        }
        IsRunning = false;
    }
}
