using Maps.Grounds.Model.Enums;
using Maps.Grounds.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : IComparable<Cell>
{
    public int X { get; set; }
        
    public int Y { get; set; }
    public float Priority { get; set; } // smaller values are higher priority

    private static readonly Cell UP = new Cell(0, -1);
    private static readonly Cell DOWN = new Cell(0, 1);
    private static readonly Cell LEFT = new Cell(-1, 0);
    private static readonly Cell RIGHT = new Cell(1, 0);

    public Cell[] Direction = new Cell[]
    {
        UP, DOWN, LEFT, RIGHT
    };

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int CompareTo(Cell cell)
    {
        if (cell == this)
            return 0;
        if (Priority < cell.Priority)
            return -1;
        return 1;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
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
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Cell a, Cell b)
    {
        return a.X != b.X && a.Y != b.Y;
    }
}
