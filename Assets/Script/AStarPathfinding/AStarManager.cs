using Maps.Grounds.Model.Enums;
using Maps.Grounds.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace AStartPathfinding
{
    public class AStarManager : SingletonMonoBehaviour<AStarManager>
    {
        private const float kAxialCost = 1;
        private const float kDiagonalCost = 1.4f;

        private PriorityQueue<Cell> m_openList = new PriorityQueue<Cell>();

        private Dictionary<int, Cell> m_closedList = new Dictionary<int, Cell>();

        private Dictionary<int, float> m_totalG = new Dictionary<int, float>();

        private MapViewModel m_viewModel;

        public void Setup(MapViewModel viewModel)
        {
            m_viewModel = viewModel;

            var start = new Cell(0, 0, m_viewModel.GetCellName(0, 0));
            var end = new Cell(6, 3, m_viewModel.GetCellName(6, 3));
            var list = Search(start, end);
            Debug.Log($"start {m_viewModel.GetCellName(0, 0)} - end {m_viewModel.GetCellName(6, 3)}");
        }


        /// <summary>
        /// g(n) Represents the exact movement cost of the path from 2 continougous cells a to b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// 
        public float G(Cell a, Cell b)
        {
            var dx = Mathf.Abs(a.X - b.X);
            var dy = Mathf.Abs(a.Y - b.Y);
            if (dx + dy == 2)
            {
                return kDiagonalCost;
            }
            return kAxialCost;
        }

        /// <summary>
        ///  h(n) Represents the heuristic estimated cost from a to goalPoint
        /// </summary>
        /// <param name="a"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public float H(Cell a, Cell goal)
        {
            var dx = Mathf.Abs(a.X - goal.X);
            var dy = Mathf.Abs(a.Y - goal.Y);
            return kAxialCost * (dx + dy) + (kDiagonalCost - 2 * kAxialCost) * Mathf.Min(dx, dy);
        }

        public IEnumerable<Cell> GetNeighbor(Cell cell)
        {
            foreach (var dir in cell.Direction)
            {
                var d = cell + dir;
                d.Name = m_viewModel.GetCellName(d);
                if (m_viewModel.GetCellType(d) > CellType.Wall)
                    yield return d;
            }
        }

        public int GetCellName(Cell cell)
        {
            return m_viewModel.GetCellName(cell);
        }

        public List<Cell> Search(Cell start, Cell goal)
        {
            start.Priority = 0;
            m_openList.Enqueue(start);
            var name = GetCellName(start);
            m_closedList.Add(name, null);
            m_totalG.Add(name, 0);
            string strOpen = "";
            string strClose = "";
            string strneight = "";

            while (m_openList.Count > 0)
            {
                Debug.Log("==========================");
                strOpen = "";
                strClose = "";
                strneight = "";
                foreach (var o in m_openList.getList())
                {
                    strOpen += " " + GetCellName(o);
                }
                Debug.Log("open: " + strOpen);
                var current = m_openList.Dequeue();

                if (current == goal)
                {
                    return CreatePath(current);
                }

                foreach (var neightbor in GetNeighbor(current))
                {
                    var neightborName = GetCellName(neightbor);
                    var currentName = GetCellName(current);
                    if (m_closedList.ContainsKey(neightborName))
                    {
                        Debug.Log("contain closedlist " + neightborName);
                        continue;
                    }
                    strneight += " " + neightborName;
                    Debug.Log("neightbor " + strneight);

                    var cost = m_totalG[currentName] + G(current, neightbor);
                    if (!m_totalG.TryGetValue(neightborName, out float value) || cost < m_totalG[neightborName])
                    {
                        if (m_totalG.TryGetValue(neightborName, out float value1))
                        {
                            m_closedList.Remove(neightborName);
                            m_totalG.Remove(neightborName);
                        }
                        m_totalG.Add(neightborName, cost);
                        neightbor.Priority = cost + H(neightbor, goal);
                        neightbor.Name = neightborName;
                        m_openList.Enqueue(neightbor);
                        m_closedList.Add(neightborName, current);
                    }
                }
            }
            return new List<Cell>();
        }

        public List<Cell> CreatePath(Cell cell)
        {
            List<Cell> list = new List<Cell>();
            while (cell != null)
            {
                list.Add(cell);
                cell = m_closedList[GetCellName(cell)];
            }
            string s = "";
            list.Reverse();
            foreach (var c in list)
                s += GetCellName(c) + " - ";
            Debug.Log(s);
            return list;
        }
    }
}