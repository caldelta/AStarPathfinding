using Cysharp.Threading.Tasks;
using Maps.Grounds.Model;
using Requests.Maps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Requests.Web
{
    public class MapRequest
    {
        private static MapRequest _instance;

        public static MapRequest Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = new MapRequest(new MapLoader());
                return _instance;
            }
        }

        private readonly MapLoader m_loader;

        private MapRequest(MapLoader loader)
        {
            m_loader = loader;
        }

        public async UniTask<Map> Get(int mapLevel)
        {
            return await m_loader.Get(mapLevel);
        }
    }
}