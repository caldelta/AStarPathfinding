using Maps.Grounds.Model;
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
            GameManager.Instance.Setup(m_viewModel.GroundPos(0, 0));
            AStarManager.Instance.Setup(m_viewModel);
            
            Init(m_viewModel);
        }
        private void Init(MapViewModel viewModel)
        {
            int count = 0;

            for (int y = 0; y < viewModel.Height; y++)
            {
                for (int x = 0; x < viewModel.Width; x++)
                {
                    var ground = Instantiate(m_groundCell, m_viewModel.GroundPos(x, y), m_groundCell.transform.rotation, transform);
                    var type = (int)m_viewModel.GetCellType(x, y);

                    ground.gameObject.name = count.ToString();
                    ground.GetComponent<MapCellView>().SetColor(type);
                    ground.GetComponent<MapCellView>().SetName(count);
                    ground.GetComponent<MapCellView>().SetType(type);

                    count++;
                }
            }
        }
    }
}
