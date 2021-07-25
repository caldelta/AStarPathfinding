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
        private Line m_line;

        [SerializeField]
        private LineRenderer m_linePrefab;
        public void Setup(MapViewModel viewModel, Player player, Line line)
        {
            m_viewModel = viewModel;
            m_player = player;
            m_line = line;
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
                m_line.LineRenderer = Instantiate(m_linePrefab, Vector3.zero, Quaternion.identity, transform);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RandomPos();
                var end = new Cell(8, 4);
                var list = AStarManager.Instance.Search(m_player.CellPos, end);
                if (list.Count == 0)
                    Debug.Log("Path not found");
                var list1 = new List<Vector3>();
                foreach(var pos in list)
                {
                    list1.Add(m_viewModel.MapPos(pos));
                }
                var array = list1.ToArray();
                m_line.LineRenderer.positionCount = array.Length;

                m_line.LineRenderer.SetPositions(array);

                Debug.Log($"start {m_viewModel.GetCellName(m_player.CellPos)} - end {m_viewModel.GetCellName(end)}");
            }
        }
    }
}