using AStartPathfinding;
using AStartPathfinding.Grounds.View;
using Cameras;
using Cysharp.Threading.Tasks;
using Games;
using Maps.Grounds.Model;
using Maps.Grounds.Model.Enums;
using Maps.Grounds.ViewModel;
using Newtonsoft.Json;
using Requests.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility;

namespace Maps
{
    public class MapManager : SingletonMonoBehaviour<MapManager>
    {
        [SerializeField]
        private GameObject m_groundCell;

        private MapViewModel m_viewModel;

        [SerializeField]
        private int m_mapLevel;
#if DEBUG
        public List<MapCellView> List = new List<MapCellView>();
        private bool isInit;
#endif
        private void Start()
        {
            Load();
        }

        private async UniTask Load()
        {
#if DEBUG
            isInit = false;
#endif
            var map = await MapRequest.Instance.Get(m_mapLevel);
            m_viewModel = new MapViewModel(map);

            Init(m_viewModel);

            CameraManager.Instance.Setup(m_viewModel.Width, m_viewModel.Height);
            AStarManager.Instance.Setup(m_viewModel);
            GameManager.Instance.Setup(m_viewModel);
        }

        public void Init(MapViewModel viewModel)
        {
#if DEBUG
            if(isInit)
            {
                foreach(var view in List)
                {
                    var cell = m_viewModel.GetCellByName(int.Parse(view.name));
                    var type = m_viewModel.GetCellType(cell.X, cell.Y);
                    view.SetColor(type);
                    Debug.Log(type);
                }
                return;                
            }
#endif
            for (int y = 0; y < viewModel.Height; y++)
            {
                for (int x = 0; x < viewModel.Width; x++)
                {
                    var type = m_viewModel.GetCellType(x, y);
                    if(type != CellType.NA)
                    {
                        var ground = Instantiate(m_groundCell, m_viewModel.WorldPos(x, y), m_groundCell.transform.rotation, transform);
                        var view = ground.GetComponent<MapCellView>();
                        var name = m_viewModel.GetCellName(x, y);

                        ground.gameObject.name = name.ToString();
                        view.SetColor(type);
                        view.SetName(name);
                        view.SetType(type);
#if DEBUG
                        List.Add(view);
#endif
                    }
                }
            }
#if DEBUG
            isInit = true;
#endif
        }
    }
}
