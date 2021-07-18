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
        private PriorityQueue<Cell> m_openList = new PriorityQueue<Cell>();

        private List<Cell> m_closedList = new List<Cell>();

        private Dictionary<Cell, float> G = new Dictionary<Cell, float>();

        private MapViewModel m_viewModel;
        public void Setup(MapViewModel viewModel)
        {
            m_viewModel = viewModel;

            var list = Search(new Cell(0, 0), new Cell(6, 3));
            foreach (var node in list)
            {
                Debug.Log(m_viewModel.GetCellName(node.X, node.Y));
            }
        }


        /// <summary>
        /// g(n) Represents the exact cost of the path from a to b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public float MovementCost(Cell a, Cell b)
        {
            return Vector2.Distance(a.ToVector2(), b.ToVector2());
        }

        /// <summary>
        ///  h(n) Represents the heuristic estimated cost from a to goalPoint
        /// </summary>
        /// <param name="a"></param>
        /// <param name="goalPoint"></param>
        /// <returns></returns>
        public float H(Vector2 a, Vector2 goalPoint)
        {
            return Vector2.Distance(a, goalPoint);
        }

        public List<Cell> GetNeighbor(Cell cell)
        {
            List<Cell> list = new List<Cell>();

            if (m_viewModel.GetCellType(cell.UP) > CellType.Wall)
                list.Add(cell.UP);

            if (m_viewModel.GetCellType(cell.DOWN) > CellType.Wall)
                list.Add(cell.DOWN);

            if (m_viewModel.GetCellType(cell.LEFT) > CellType.Wall)
                list.Add(cell.LEFT);

            if (m_viewModel.GetCellType(cell.RIGHT) > CellType.Wall)
                list.Add(cell.RIGHT);

            return list;
        }

        
        public List<Cell> Search(Cell start, Cell end)
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
            
            while(m_openList.Count > 0)
            {
                var current = m_openList.Dequeue();

                if (current == end)
                {
                    return m_closedList;
                }
                    
                m_closedList.Add(current);

                foreach(var neightbor in GetNeighbor(current))
                {
                    var cost = G[current] + MovementCost(current, neightbor);
                    //if next not in cost_so_far or new_cost < cost_so_far[next]:
                    if(!G.TryGetValue(neightbor, out float value) || cost < G[neightbor])
                    {
                        //         cost_so_far[next] = new_cost
                        //         priority = new_cost + heuristic(goal, next)
                        //         frontier.put(next, priority)
                        //         came_from[next] = current
                        G[neightbor] = cost;
                        m_openList.Enqueue(neightbor);
                        m_closedList.Add(neightbor);
                    }
                }
            }

            return new List<Cell>();
        }
    }
}