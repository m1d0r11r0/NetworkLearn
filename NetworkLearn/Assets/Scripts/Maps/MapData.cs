using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class MapData : ScriptableObject
{
    [System.Serializable]
    public class LoadTypeList
    {
        [SerializeField]
        private LoadType[] list;

        public LoadTypeList(int size)
        {
            list = new LoadType[size];
        }

        public LoadType this[int i]
        {
            get { return list[i]; }

            set { list[i] = value; }
        }
    }
    /// <summary>
    /// 読み込み時に参照するインデックス用enum
    /// </summary>
    public enum LoadType
    {
        Road = 0,
        TreeDef,
        Wall,
        Player1,
        Player2,
        TreeTop,
        TreeDown,
        TreeRight,
        TreeLeft,
        Item01,
        Item02,
        Item03,
        Item04,
        Item05,
        Item06,
        Item07,
        Item08,
    }

    /// <summary>
    /// プレイ時に参照するマスの種類
    /// </summary>
    public enum Type
    {
        // TODO:インゲームでのマップデータの持ち方を考える
    }

    [SerializeField] private string _MapName;       // マップ名
    [SerializeField] private Vector2Int _MapSize;   // マップのサイズ
    [SerializeField] private LoadTypeList[] _LoadData;    // 初期化設定用マップジャグ配列

    public string MapName
    {
        get { return _MapName; }
        set { _MapName = value; }
    }

    public Vector2Int MapSize
    {
        get { return _MapSize; }
        set { _MapSize = value; }
    }

    public LoadTypeList[] LoadData
    {
        get { return _LoadData; }
        set { _LoadData = value; }
    }
}