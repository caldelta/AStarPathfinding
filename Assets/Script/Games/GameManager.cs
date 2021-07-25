using AStartPathfinding;
using Maps.Grounds.Model.Enums;
using Maps.Grounds.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Games
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [SerializeField]
        private GameObject m_playerPrefab;

        private MapViewModel m_viewModel;

        private Player m_player;
        public void Setup(MapViewModel viewModel, Player player)
        {
            m_viewModel = viewModel;
            m_player = player;
        }
        private void RandomPos()
        {
            var name = Random.Range(0, 99);
            var cell = m_viewModel.GetCellByName(name);

            while (m_viewModel.GetCellType(cell) <= CellType.Wall)
            {
                name = Random.Range(0, 99);
                cell = m_viewModel.GetCellByName(name);
            }
#if DEBUG
            Debug.Log($"{name} - {m_viewModel.GetCellType(cell)}");
#endif
            if (m_player.GameObject != null)
            {
                m_player.WorldPos = m_viewModel.MapPos(cell.X, cell.Y);
                m_player.CellPos = cell;
            }
            else
            {
                m_player.GameObject = Instantiate(m_playerPrefab, m_viewModel.MapPos(cell.X, cell.Y), Quaternion.identity, transform);
                m_player.CellPos = cell;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RandomPos();
                AStarManager.Instance.Setup(m_viewModel, m_player);
                var end = new Cell(8, 4);
                var list = AStarManager.Instance.Search(m_player.CellPos, end);
                if (list.Count == 0)
                    Debug.Log("Path not found");
                Debug.Log($"start {m_viewModel.GetCellName(m_player.CellPos)} - end {m_viewModel.GetCellName(end)}");
            }
        }
    }
}