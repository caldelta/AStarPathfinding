using AStartPathfinding.Model;
using Maps.Grounds.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput
{
    public static float MousePosX = 0;
    public static float MousePosY = 0;
    public static float CellY = 0;
    public static float CellX = 0;

    public static Cell ToCellPos(Vector3 mousePos, MapViewModel viewModel)
    {
        var pos = Camera.main.ScreenToWorldPoint(mousePos);

        var intPosX = (float)Math.Truncate(pos.x);
        var intPosY = (float)Math.Truncate(pos.z);

        var decimalPosX = Mathf.Abs(pos.x - intPosX);
        var decimalPosY = Mathf.Abs(pos.z - intPosY);

        if (Math.Abs(decimalPosX) > 0.5f)
        {
            MousePosX = intPosX + 1;
        }
        else
        {
            MousePosX = intPosX;
        }

        if (Math.Abs(decimalPosY) > 0.5f)
        {
            MousePosY = intPosY + 1;
        }
        else
        {
            MousePosY = intPosY;
        }

        MousePosX = Mathf.Clamp(MousePosX, 0, viewModel.Width - 1);
        MousePosY = Mathf.Clamp(MousePosY, 0, viewModel.Width - 1);

        CellY = viewModel.Width - 1 - MousePosY;

        CellX = MousePosX;

        return new Cell(CellX, CellY);
    }
}
