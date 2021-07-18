using AStartPathfinding.Grounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace AStartPathfinding
{
    public class CameraManager : SingletonMonoBehaviour<CameraManager>
    {
        // Start is called before the first frame update

        public void Setup(int mapWidth, int mapHeight)
        {
            transform.position = new Vector3(mapWidth >> 1, 15, mapHeight >> 1);
            transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
        }
    }
}