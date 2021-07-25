﻿using Games;
using Maps.Grounds.Model;
using Maps.Grounds.Model.Enums;
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
            GameManager.Instance.Setup(m_viewModel);
            AStarManager.Instance.Setup(m_viewModel);
            
            Init(m_viewModel);
        }
        private void Init(MapViewModel viewModel)
        {
            for (int y = 0; y < viewModel.Height; y++)
            {
                for (int x = 0; x < viewModel.Width; x++)
                {
                    var type = m_viewModel.GetCellType(x, y);
                    if(type != CellType.NA)
                    {
                        var ground = Instantiate(m_groundCell, m_viewModel.MapPos(x, y), m_groundCell.transform.rotation, transform);
                        var name = m_viewModel.GetCellName(x, y);

                        ground.gameObject.name = name.ToString();
                        ground.GetComponent<MapCellView>().SetColor(type);
                        ground.GetComponent<MapCellView>().SetName(name);
                        ground.GetComponent<MapCellView>().SetType((int)type);
                    }                    
                }
            }
        }
    }
}
