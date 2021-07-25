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

        private Player m_player;

        private MapViewModel m_viewModel;

        public void Setup(MapViewModel viewModel)
        {
            m_viewModel = viewModel;
            m_player = new Player();
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
                m_player.Position = m_viewModel.MapPos(cell.X, cell.Y);
            }
            else
            {
                m_player.GameObject = Instantiate(m_playerPrefab, m_viewModel.MapPos(cell.X, cell.Y), Quaternion.identity, transform);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RandomPos();
            }
        }
    }
}