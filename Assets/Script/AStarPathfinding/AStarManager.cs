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

        private MapViewModel m_viewModel;

        public void Setup(MapViewModel viewModel)
        {
            m_viewModel = viewModel;

            var start = new Cell(0, 3);
            //var end = new Cell(6, 3);
            var end = new Cell(8, 8);
            var list = Search(start, end);
            if (list.Count == 0)
                Debug.Log("Path not found");
            Debug.Log($"start {m_viewModel.GetCellName(0, 0)} - end {m_viewModel.GetCellName(8, 8)}");
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
                d.Name = GetCellName(d);
                if (GetCellType(d) > CellType.Wall && !m_closedList.ContainsKey(d.Name))
                    yield return d;
            }
        }

        public int GetCellName(Cell cell)
        {
            return m_viewModel.GetCellName(cell);
        }

        public CellType GetCellType(Cell cell)
        {
            return m_viewModel.GetCellType(cell);
        }

        public List<Cell> Search(Cell start, Cell goal)
        {
            m_openList.Clear();
            m_closedList.Clear();
            var name = GetCellName(start);
            start.F = 0;
            start.G = 0;
            start.Name = name;
            m_openList.Enqueue(start);
            m_closedList.Add(name, null);
#if DEBUG
            string strOpen = "";
            string strClose = "";
            string strneight = "";
#endif
            while (m_openList.Count > 0)
            {
#if DEBUG
                Debug.Log("==========================");
                strOpen = "";
                strClose = "";
                strneight = "";
                foreach (var o in m_openList.getList())
                {
                    strOpen += " " + GetCellName(o);
                }
                Debug.Log("open: " + strOpen);
#endif
                var current = m_openList.Dequeue();

                if (current == goal)
                {
                    return CreatePath(current);
                }

                foreach (var neightbor in GetNeighbor(current))
                {
#if DEBUG
                    strneight += " " + neightbor.Name;
                    Debug.Log("neightbor " + strneight);
#endif

                    var newG = current.G + G(current, neightbor);
                    if (!m_closedList.TryGetValue(neightbor.Name, out Cell value) || newG < neightbor.G)
                    {
                        neightbor.G = newG;
                        neightbor.F = newG + H(neightbor, goal);
                        m_openList.Enqueue(neightbor);
                        m_closedList.Add(neightbor.Name, current);
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
                cell = m_closedList[cell.Name];
            }
            string s = "";
            list.Reverse();
#if DEBUG
            foreach (var c in list)
                s += GetCellName(c) + " - ";
            Debug.Log(s);
#endif
            return list;
        }
    }
}