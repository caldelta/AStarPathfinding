using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility;

namespace AStartPathfinding.Grounds
{
    public class GroundManager : SingletonMonoBehaviour<GroundManager>
    {
        [SerializeField]
        public int Width { get; set; } = 10;

        [SerializeField]
        public int Height { get; set; } = 10;

        [SerializeField]
        private GameObject m_groundCell;
        
        [SerializeField]
        private GameObject m_groundNumber;

        private readonly Color BLUE = new Color(0, 0, 1, 0.1f);
        private readonly Color YELLOW = new Color(1, 1, 0, 0.1f);

        private void Start()
        {
            Init();
        }
        private void Init()
        {
            int count = 0;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                        var ground = Instantiate(m_groundCell, new Vector3(i, 0, j), Quaternion.identity, transform);
                        ground.GetComponent<Renderer>().material.color = (i + j) % 2 == 0 ? BLUE : YELLOW;
                        ground.GetComponent<Ground>().SetNumber(count);
                        count++;
                }
            }
        }
    }
}
