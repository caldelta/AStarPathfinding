using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maps.Grounds.Model
{
    [System.Serializable]
    public class Map
    {
        public int Width;
        public int Height;
        public List<int> Data;
    }
}