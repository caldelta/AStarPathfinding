using AStartPathfinding.Model;
using Cysharp.Threading.Tasks;
using Maps.Grounds.Model;
using Maps.Grounds.Model.Enums;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Maps.Grounds.ViewModel
{
    /// <summary>
    /// Map (0,0) start from Top Left
    /// </summary>
    public class MapViewModel
    {
        public int Width
        {
            get
            {
                return m_map.Width;
            }
        }
        public int Height
        {
            get
            {
                return m_map.Height;
            }
        }

        public int Size
        {
            get
            {
                return m_map.Data.Length;
            }
        }

        public int[] Map
        {
            get
            {
                return m_map.Data;
            }
        }

        private Map m_map;
                
        public MapViewModel(Map map)
        {
            m_map = map;
#if DEBUG
            Debug.Log($"Map size {Width}x{Height} total cell = {m_map.Data.Length}");
#endif
        }

        /// <summary>
        /// Get cell type from json map file by x,y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CellType GetCellType(float x, float y)
        {
            return ToCellType(m_map.Data[GetCellName(x, y)]);
        }
        /// <summary>
        /// Get cell type by cell object
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public CellType GetCellType(Cell cell)
        {
            if (!IsInMap(cell))
                return CellType.NA;

            return ToCellType(m_map.Data[GetCellName(cell)]);
        }
        private bool IsInMap(Cell cell)
        {
            if (cell.X >= Width || cell.X < 0)
                return false;
            if (cell.Y >= Height || cell.Y < 0)
                return false;
            return true;

        }

        public static CellType ToCellType(int type) => type switch
        {
            0 => CellType.NA,
            1 => CellType.Wall,
            2 => CellType.Ground,
            _ => throw new System.NotImplementedException()
        };

        /// <summary>
        /// Convert origin (0,0) from botleft to topleft World Position of the cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector3 WorldPos(float x, float y)
        {
            return new Vector3(x, 0, Height - y - 1);
        }

        /// <summary>
        /// Convert origin (0,0) from botleft to topleft
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public Vector3 MapPos(Cell cell)
        {
            return new Vector3(cell.X, 0, Height - cell.Y - 1);
        }

        /// <summary>
        /// Get Cell name by x,y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetCellName(float x, float y)
        {
            return (int)x + (int)y * Height;
        }
        /// <summary>
        /// Get Cell name by obj
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public int GetCellName(Cell cell)
        {
            return (int)cell.X + (int)cell.Y * Height;
        }

        public int GetCellName(Vector2 cell)
        {
            return (int)cell.x + (int)cell.y * Height;
        }

        public Cell GetCellByName(int name)
        {
            if (name < 0)
            {
                return new Cell(0, 0);
            }
            if (name > Width * Width - 1)
            {
                return new Cell(Width - 1, Width - 1);
            }
            return new Cell(name % Width, name / Width);
        }
    }
}