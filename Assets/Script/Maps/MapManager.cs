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

        [SerializeField]
        private Material m_matBlue;

        [SerializeField]
        private Material m_matYellow;

        private MapViewModel m_viewModel;
        private void Start()
        {
            m_viewModel = new MapViewModel();

            CameraManager.Instance.Setup(m_viewModel.Width, m_viewModel.Height);
            Init(m_viewModel);
        }
        private void Init(MapViewModel viewModel)
        {
            int count = 0;

            for (int i = 0; i < viewModel.Width; i++)
            {
                for (int j = 0; j < viewModel.Height; j++)
                {
                    var ground = Instantiate(m_groundCell, new Vector3(i, 0, j), m_groundCell.transform.rotation, transform);
                    ground.gameObject.name = count.ToString();
                    ground.GetComponent<Renderer>().material = (i + j) % 2 == 0 ? m_matBlue : m_matYellow;
                    ground.GetComponent<GroundView>().SetNumber(count);
                    count++;
                }
            }
        }
    }
}
