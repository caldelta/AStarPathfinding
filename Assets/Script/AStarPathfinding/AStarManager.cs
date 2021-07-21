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

        private Dictionary<Cell, Cell> m_closedList = new Dictionary<Cell, Cell>();

        private Dictionary<Cell, float> m_totalG = new Dictionary<Cell, float>();

        private MapViewModel m_viewModel;
        public void Setup(MapViewModel viewModel)
        {
            m_viewModel = viewModel;

            var list = Search(new Cell(0, 0), new Cell(6, 3));
            Debug.Log($"start {m_viewModel.GetCellName(0, 0)} - end {m_viewModel.GetCellName(6, 3)}");
            foreach (var node in list)
            {
                Debug.Log(m_viewModel.GetCellName(node.Value.X, node.Value.Y));
            }
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
            if(dx + dy == 2)
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
            foreach(var dir in cell.Direction)
            {
                var d = cell + dir;
                if (m_viewModel.GetCellType(d) > CellType.Wall)
                    yield return d;
            }
        }

        
        public Dictionary<Cell, Cell> Search(Cell start, Cell goal)
        {
            //frontier = PriorityQueue()
            //frontier.put(start, 0)
            //came_from = dict()
            //cost_so_far = dict()
            //came_from[start] = None
            //cost_so_far[start] = 0

            //while not frontier.empty():
            //    current = frontier.get()

            //   if current == goal:
            //    break

            //   for next in graph.neighbors(current):
            //      new_cost = cost_so_far[current] + graph.cost(current, next)
            //      if next not in cost_so_far or new_cost < cost_so_far[next]:
            //         cost_so_far[next] = new_cost
            //         priority = new_cost + heuristic(goal, next)
            //         frontier.put(next, priority)
            //         came_from[next] = current

            m_openList.Enqueue(start);
            start.Priority = 0;
            m_closedList[start] = start;
            m_totalG[start] = 0;

            while(m_openList.Count > 0)
            {
                var current = m_openList.Dequeue();

                if (current == goal)
                {
                    return m_closedList;
                }
                //m_closedList.Add(current, current);

    //            if neighbor in OPEN and cost less than g(neighbor):
    //              remove neighbor from OPEN, because new path is better
    //            if neighbor in CLOSED and cost less than g(neighbor):
    //              remove neighbor from CLOSED
    //            if neighbor not in OPEN and neighbor not in CLOSED:
    //              set g(neighbor) to cost
    //              add neighbor to OPEN
    //              set priority queue rank to g(neighbor) +h(neighbor)
    //              set neighbor's parent to current
                foreach (var neightbor in GetNeighbor(current))
                {
                    var cost = m_totalG[current] + G(current, neightbor);
                    //if next not in cost_so_far or new_cost < cost_so_far[next]:
                    if(!m_totalG.TryGetValue(neightbor, out float value) || cost < m_totalG[neightbor])
                    {
                        //         cost_so_far[next] = new_cost
                        //         priority = new_cost + heuristic(goal, next)
                        //         frontier.put(next, priority)
                        //         came_from[next] = current
                        if(m_totalG.TryGetValue(neightbor, out float value1))
                        {
                            m_closedList[neightbor] = current;
                            m_totalG[neightbor] = cost;
                        }
                        else
                        {
                            m_totalG[neightbor] = cost;
                            neightbor.Priority = cost + H(neightbor, goal);
                            m_openList.Enqueue(neightbor);
                            m_closedList.Add(neightbor, current);
                        }                        
                    }
                }
            }
            return new Dictionary<Cell, Cell>();
        }
    }
}