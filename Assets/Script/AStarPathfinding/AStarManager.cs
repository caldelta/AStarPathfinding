using Maps.Grounds.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace AStartPathfinding
{
    public class AStarManager : SingletonMonoBehaviour<AStarManager>
    {
        public void Setup(MapViewModel viewModel)
        {

        }

        /// <summary>
        /// g(n) Represents the exact cost of the path from a to b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public float G(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        /// <summary>
        ///  h(n) Represents the heuristic estimated cost from a to goalPoint
        /// </summary>
        /// <param name="a"></param>
        /// <param name="goalPoint"></param>
        /// <returns></returns>
        public float H(Vector2 a, Vector2 goalPoint)
        {
            return Vector2.Distance(a, goalPoint);
        }
    }
}