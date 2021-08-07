using AStartPathfinding.Model;
using Maps;
using Maps.Grounds.Model.Enums;
using Maps.Grounds.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using System.Linq;

namespace AStartPathfinding
{
    public class AStarManager : SingletonMonoBehaviour<AStarManager>
    {
        private const float kAxialCost = 1;
        private const float kDiagonalCost = 1.5f;

        private PriorityQueue<Cell> m_openList = new PriorityQueue<Cell>();

        private Dictionary<int, Cell> m_closedList = new Dictionary<int, Cell>();

        private MapViewModel m_viewModel;

        [SerializeField]
        private bool isPathSmoothing;
        public void Setup(MapViewModel viewModel)
        {
            m_viewModel = viewModel;
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

        private bool IsVisit(Cell cell)
        {
            return m_closedList.ContainsKey(cell.Name);
        }

        private bool IsWalkable(Cell cell)
        {
            var type = m_viewModel.GetCellType(cell);
            return type > CellType.Wall && type <= CellType.Ground;
        }

        public IEnumerable<Cell> GetNeighbor(Cell cell)
        {
            var upLeft = cell + Cell.UPLEFT;
            upLeft.Name = GetCellName(upLeft);

            var up = cell + Cell.UP;
            up.Name = GetCellName(up);

            var upRight = cell + Cell.UPRIGHT;
            upRight.Name = GetCellName(upRight);

            var downLeft = cell + Cell.DOWNLEFT;
            downLeft.Name = GetCellName(downLeft);

            var down = cell + Cell.DOWN;
            down.Name = GetCellName(down);

            var downRight = cell + Cell.DOWNRIGHT;
            downRight.Name = GetCellName(downRight);

            var left = cell + Cell.LEFT;
            left.Name = GetCellName(left);

            var right = cell + Cell.RIGHT;
            right.Name = GetCellName(right);

            if (GetCellType(up) > CellType.Wall && !IsVisit(up))
            {
                yield return up;
            }

            if (GetCellType(down) > CellType.Wall && !IsVisit(down))
            {
                yield return down;
            }

            if (GetCellType(left) > CellType.Wall && !IsVisit(left))
            {
                yield return left;
            }

            if (GetCellType(right) > CellType.Wall && !IsVisit(right))
            {
                yield return right;
            }

            if ((GetCellType(up) > CellType.Wall || GetCellType(right) > CellType.Wall) && GetCellType(upRight) > CellType.Wall &&
                !IsVisit(upRight)
                )
            {
                yield return upRight;
            }

            if ((GetCellType(up) > CellType.Wall || GetCellType(left) > CellType.Wall) && GetCellType(upLeft) > CellType.Wall! &&
                !IsVisit(upLeft)
                )
            {
                yield return upLeft;
            }

            if ((GetCellType(down) > CellType.Wall || GetCellType(right) > CellType.Wall) && GetCellType(downRight) > CellType.Wall &&
                !IsVisit(downRight)
                )
            {
                yield return downRight;
            }

            if ((GetCellType(down) > CellType.Wall || GetCellType(left) > CellType.Wall) && GetCellType(downLeft) > CellType.Wall &&
                !IsVisit(downLeft)
                )
            {
                yield return downLeft;
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
            int step = 0;
            MapManager.Instance.Init(m_viewModel);
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
                step++;
#endif
                var current = m_openList.Dequeue();
#if DEBUG
                MapManager.Instance.List.FirstOrDefault(c => c.name == m_viewModel.GetCellName(current).ToString()).SetColor(CellType.Open);
#endif
                if (current == goal)
                {
#if DEBUG
                    Debug.Log("step : " + step);
#endif
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
        private List<Cell> PathSmoothing(List<Cell> list)
        {
            var nodeExamCount = 3;

            for (int i = 0; i < list.Count - nodeExamCount; i++)
            {
                var node0 = list[i];
                var node1 = list[i + 1];
                var node2 = list[i + 2];

                bool diagonal = false;

                if (node0.X != node2.X && node0.Y != node2.Y)
                {
                    diagonal = true;
                }

                if(m_viewModel.GetCellType(node1) > CellType.Wall)
                {
                    var name0 = m_viewModel.GetCellName(node0);
                    var nodeTemp = m_viewModel.GetCellByName(name0 + 1);

                    if(!IsWalkable(nodeTemp))
                    {
                        diagonal = true;
                    }
                }

                if (!diagonal)
                {
                    list.Remove(node1);
                }
            }
            return list;
        }

        public List<Cell> CreatePath(Cell cell)
        {
            List<Cell> list = new List<Cell>();
            while (cell != null)
            {
                list.Add(cell);
                cell = m_closedList[cell.Name];
            }
            list.Reverse();
#if DEBUG

            string s = "";
            foreach (var p in list)
            {
                s += " " + p.Name;
            }
            Debug.Log(s);
            Debug.Log("****************");

#endif
            if (isPathSmoothing)
                PathSmoothing(list);
#if DEBUG
            s = "";           
            foreach(var p in list)
            {
                s += " " + p.Name;
            }
            Debug.Log(s);
#endif
            return list;
        }
    }
}