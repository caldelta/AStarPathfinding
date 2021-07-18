using Maps.Grounds.Model.Enums;
using Maps.Grounds.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : IComparable
{
    public int X;
    public int Y;
    
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        Cell cell = obj as Cell;

        if (X == cell.X && Y == cell.Y)
            return 0;
        if (X < cell.X && Y < cell.Y)
            return -1;
        return 1;
    }

    public Cell UP
    {
        get
        {            
            return new Cell(X, Y - 1);
        }
    }

    public Cell DOWN
    {
        get
        {
            return new Cell(X, Y + 1);
        }
    }

    public Cell LEFT
    {
        get
        {
            return new Cell(X -1, Y);
        }
    }

    public Cell RIGHT
    {
        get
        {
            return new Cell(X + 1, Y);
        }
    }
}
