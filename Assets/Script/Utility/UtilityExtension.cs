using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class UtilityExtension
    {
        public static Vector2 ToVector2(this Cell cell)
        {
            return new Vector2(cell.X, cell.Y);
        }
    }
}