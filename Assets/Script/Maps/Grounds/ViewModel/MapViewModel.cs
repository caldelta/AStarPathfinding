using Maps.Grounds.Model;
using Maps.Grounds.Model.Enums;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
                return m_map.Data.Count;
            }
        }

        public List<int> Map
        {
            get
            {
                return m_map.Data;
            }
        }

        private Map m_map;

        private const string kMap = "/Maps/map{0}.json";

        public MapViewModel()
        {
            LoadMap(Application.dataPath + string.Format(kMap, 1));
        }

        private void LoadMap(string mapPath)
        {
            using (StreamReader r = new StreamReader(mapPath))
            {
                string json = r.ReadToEnd();
                m_map = JsonConvert.DeserializeObject<Map>(json);
            }
#if DEBUG
            Debug.Log($"Map size {Width}x{Height} total cell = {m_map.Data.Count}");
#endif
        }

        /// <summary>
        /// Get cell type from json map file
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CellType GetCellType(int x, int y)
        {
            return ToCellType(m_map.Data[x + y * Width]);
        }
        public static CellType ToCellType(int type) => type switch
        {
            0 => CellType.Ground,
            1 => CellType.Wall,
            _ => throw new System.NotImplementedException()
        };

        /// <summary>
        /// Convert origin (0,0) from botleft to topleft
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector3 GroundPos(int x, int y)
        {
            return new Vector3(x, 0, Height - y - 1);
        }
    }
}