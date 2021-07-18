using AStartPathfinding.Grounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStartPathfinding
{
    public class CenterCameraPosition : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(MapManager.Instance.Width >> 1, 15, MapManager.Instance.Height >> 1);
            transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
        }
    }
}