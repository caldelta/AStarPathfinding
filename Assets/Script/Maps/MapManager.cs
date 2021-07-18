﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using Utility;

namespace AStartPathfinding.Grounds
{
    public class MapManager : SingletonMonoBehaviour<MapManager>
    {
        [SerializeField]
        public int Width { get; set; } = 10;

        [SerializeField]
        public int Height { get; set; } = 10;

        [SerializeField]
        private GameObject m_groundCell;  

        [SerializeField]
        private Material m_matBlue;

        [SerializeField]
        private Material m_matYellow;

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
                    var ground = Instantiate(m_groundCell, new Vector3(i, 0, j), m_groundCell.transform.rotation, transform);
                    ground.gameObject.name = count.ToString();
                    ground.GetComponent<Renderer>().material = (i + j) % 2 == 0 ? m_matBlue : m_matYellow;
                    ground.GetComponent<GroundView>().SetNumber(count);
                    count++;
                }
            }
        }

        private void LoadMap(string mapPath)
        {
            using (StreamReader r = new StreamReader("Maps/map1.json"))
            {
                string json = r.ReadToEnd();
                //List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
            }
        }
    }
}