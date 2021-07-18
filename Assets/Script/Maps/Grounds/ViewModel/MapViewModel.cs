using Maps.Grounds.Model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Maps.Grounds.ViewModel
{
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
    }
}