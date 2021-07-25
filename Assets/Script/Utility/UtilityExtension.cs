using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class UtilityExtension
    {
        public static Vector3 ToVector3(this Cell cell)
        {
            return new Vector3(cell.X, 0, cell.Y);
        }

        public static Vector3[] ToVector3Array(this List<Cell> list)
        {
            Vector3[] array = new Vector3[list.Count];

            for(int i = 0; i < list.Count; i++)
            {
                array[i] = list[0].ToVector3();
            }
            return array;
        }

    }
}