using Maps.Grounds.Model.Enums;
using Maps.Grounds.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStartPathfinding.Model
{
    public class Cell : IComparable<Cell>
    {
        public float X { get; set; }

        public float Y { get; set; }
        public float F { get; set; } // smaller values are higher priority
        public int Name { get; set; }

        public float G { get; set; }

        private static readonly Cell UPLEFT = new Cell(-1, -1);
        private static readonly Cell UP = new Cell(0, -1);
        private static readonly Cell UPRIGHT = new Cell(1, -1);
        private static readonly Cell DOWNLEFT = new Cell(-1, 1);
        private static readonly Cell DOWN = new Cell(0, 1);
        private static readonly Cell DOWNRIGHT = new Cell(1, 1);
        private static readonly Cell LEFT = new Cell(-1, 0);
        private static readonly Cell RIGHT = new Cell(1, 0);

        public Cell[] Direction = new Cell[]
        {
            UPLEFT, UP, UPRIGHT, DOWNLEFT, DOWN, DOWNRIGHT, LEFT, RIGHT 
        };

        public Cell(float x, float y)
        {
            X = x;
            Y = y;
        }


        public int CompareTo(Cell cell)
        {
            if (cell == this)
                return 0;
            if (F < cell.F)
                return -1;
            return 1;
        }

        public override bool Equals(object obj)
        {
            var cell = obj as Cell;
            return X == cell.X && Y == cell.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static Cell operator +(Cell a, Cell b)
        {
            return new Cell(a.X + b.X, a.Y + b.Y);
        }

        public static bool operator ==(Cell a, Cell b)
        {
            if (ReferenceEquals(a, null))
            {
                if (ReferenceEquals(b, null))
                {
                    return true;
                }
                return false;
            }

            if (ReferenceEquals(b, null))
            {
                if (ReferenceEquals(a, null))
                {
                    return true;
                }
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Cell a, Cell b)
        {
            return !(a == b);
        }
    }
}