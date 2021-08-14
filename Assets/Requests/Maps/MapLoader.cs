using Cysharp.Threading.Tasks;
using Factory.Maps;
using Maps.Grounds.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Requests.Maps
{
    public class MapLoader
    {
        private const string kPath = "map{0}";
        
        public MapLoader()
        {
        }

        public async UniTask<TextAsset> LoadMap(string path)
        {
            var json = await Addressables.LoadAssetAsync<TextAsset>(path);
            return json;
        }
        public async UniTask<Map> Get(int mapLevel)
        {
            var path = string.Format(kPath, mapLevel);
            var jsonString = await LoadMap(path);
            return Make(jsonString.text);
        }

        private Map Make(string jsonString)
        {
            return MapFactory.Make(jsonString);
        }
    }
}