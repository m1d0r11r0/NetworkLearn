using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : ScriptableObject
{
    private string _Name;
    private Vector2Int _MapSize;
    private int[][] _Data;

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public Vector2Int MapSize
    {
        get { return _MapSize; }
        set { _MapSize = value; }
    }

    public int[][] Data
    {
        get { return _Data; }
        set { _Data = value; }
    }
}
