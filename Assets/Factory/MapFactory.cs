using Maps.Grounds.Model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory.Maps
{
    public class MapFactory
    {
        public static Map Make(string jsonString)
        {           
            return JsonConvert.DeserializeObject<Map>(jsonString);
        }
    }
}