﻿using Maps.Grounds.Model;
using Maps.Grounds.ViewModel;
using Newtonsoft.Json;
using System.Collections;
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
        private GameObject m_groundCell;  
        
        private MapViewModel m_viewModel;
        private void Start()
        {
            m_viewModel = new MapViewModel();

            CameraManager.Instance.Setup(m_viewModel.Width, m_viewModel.Height);
            Init(m_viewModel);
            GameManager.Instance.CreatePlayer(m_viewModel.GroundPos(0, 0));

        }
        private void Init(MapViewModel viewModel)
        {
            int count = 0;

            for (int y = 0; y < viewModel.Height; y++)
            {
                for (int x = 0; x < viewModel.Width; x++)
                {
                    var ground = Instantiate(m_groundCell, viewModel.GroundPos(x, y), m_groundCell.transform.rotation, transform);
                    var type = (int)m_viewModel.GetCellType(x, y);

                    ground.gameObject.name = count.ToString();
                    ground.GetComponent<GroundView>().SetColor(type);
                    ground.GetComponent<GroundView>().SetName(count);
                    ground.GetComponent<GroundView>().SetType(type);

                    count++;
                }
            }
        }
    }
}
