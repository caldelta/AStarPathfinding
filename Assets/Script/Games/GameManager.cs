using AStartPathfinding;
using AStartPathfinding.Model;
using Games.Model;
using Maps.Grounds.Model.Enums;
using Maps.Grounds.ViewModel;
using System;
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
        private Player m_player;

        private MapViewModel m_viewModel;

        [SerializeField]
        private LineRenderer m_linePrefab;
        private Line m_line;

        public void Setup(MapViewModel viewModel)
        {
            m_viewModel = viewModel;
            m_player = new Player();
            m_line = new Line();
        }

        public Vector2 RandomPos()
        {
            var name = UnityEngine.Random.Range(0, 99);
            var cell = m_viewModel.GetCellByName(name);

            while (m_viewModel.GetCellType(cell.x, cell.y) <= CellType.Wall)
            {
                name = UnityEngine.Random.Range(0, 99);
                cell = m_viewModel.GetCellByName(name);
            }
            return cell; 
        }

        private void CreatePlayer()
        {
            var start = RandomPos();
            var x = start.x;
            var y = start.y;
#if DEBUG
            Debug.Log($"{m_viewModel.GetCellName(start)} - {m_viewModel.GetCellType(x, y)}");
#endif
            if (m_player.GameObject != null)
            {
                m_player.WorldPos = m_viewModel.WorldPos(x, y);
                m_player.WorldPos = m_viewModel.WorldPos(1, 3);
                //m_player.CellPos = new Cell(x, y);
                m_player.CellPos = new Cell(1, 3);
            }
            else
            {
                m_player.GameObject = Instantiate(m_playerPrefab, m_viewModel.WorldPos(1, 3), Quaternion.identity, transform);
                //m_player.CellPos = new Cell(x, y);
                m_player.CellPos = new Cell(1, 3);
                m_line.LineRenderer = Instantiate(m_linePrefab, Vector3.zero, Quaternion.identity, transform);
            }
        }

        public Vector3[] ConvertToArray(List<Cell> list)
        {
            Vector3[] array = new Vector3[list.Count];
            for(int i = 0; i < list.Count; i++)            
            {                
                array[i] = m_viewModel.MapPos(list[i]);
            }
            return array;
        }
        private void Update()
        {           
            if(Input.GetMouseButtonUp(0) && !m_player.IsRunning)
            {
                var screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
                var cellPos = TouchInput.ToCellPos(screenPos, m_viewModel);
                CreatePlayer();
                var list = AStarManager.Instance.Search(m_player.CellPos, cellPos);
                if (list.Count == 0)
                    Debug.Log("Path not found");

                var path = ConvertToArray(list);

                m_line.SetPositions(path);
                m_player.Run(path);
                Debug.Log($"start {m_viewModel.GetCellName(m_player.CellPos)} - end {m_viewModel.GetCellName(cellPos)}");
            }            
        }                
    }
}