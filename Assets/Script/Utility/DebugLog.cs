using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class DebugLog
    {
        public static void Yellow(object message)
        {
            Debug.Log($"<color='yellow'>{message}</color>");
        }
    }
}