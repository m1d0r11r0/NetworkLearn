using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    private const float BLOCK_SIZE = 1f;        // 各オブジェクトのサイズ

    [SerializeField] MapData[] _Maps;           // マップ用ScriptableObject配列
    [SerializeField] GameObject _WallPref;      // 壁用プレハブ
    [SerializeField] GameObject _TreePref;      // 樹木プレハブ
    [SerializeField] GameObject _FloorPref;     // 床用プレハブ
    [SerializeField] Vector3 _MapCenter;        // マップの中心点

    private Transform _Transform;       // 自分のTransformキャッシュ
    private Transform _FloorRoot;       // 床オブジェクトのルート
    private Transform _WallRoot;        // 壁オブジェクトのルート
    private Transform _TreeRoot;        // 樹木オブジェクトのルート

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Init()
    {
        // 各ルート用オブジェクトの生成
        _Transform = transform;
        _FloorRoot = new GameObject("FloorRoot").transform;
        _FloorRoot.transform.parent = _Transform;
        _WallRoot = new GameObject("WallRoot").transform;
        _WallRoot.transform.parent = _Transform;
        _TreeRoot = new GameObject("TreeRoot").transform;
        _TreeRoot.transform.parent = _Transform;

#if false
        // NOTE:仮でマップロード処理の配置
        LoadMap(0);
#endif
    }

    /// <summary>
    /// マップの読み込み処理
    /// </summary>
    /// <param name="stageIdx">ステージのインデックス</param>
    public void LoadMap(int stageIdx)
    {
        if (stageIdx >= _Maps.Length)
        {
            Debug.LogError($"不正なインデックス値です\nstageIdx : {stageIdx}");
            return;
        }

        var loadMap = _Maps[stageIdx];

        // マップの基準座標(左上)の計算
        var mapRootPos = new Vector3();
        mapRootPos.x = _MapCenter.x - loadMap.MapSize.x * BLOCK_SIZE / 2f;
        mapRootPos.y = _MapCenter.y;
        mapRootPos.z = _MapCenter.z + loadMap.MapSize.y * BLOCK_SIZE / 2f;

        // 現在生成しているオブジェクトの座標
        var currentMapPos = mapRootPos;

        for (int y = 0; y < loadMap.MapSize.y; y++)
        {
            currentMapPos.z = mapRootPos.z - y * BLOCK_SIZE;
            for (int x = 0; x < loadMap.MapSize.x; x++)
            {
                currentMapPos.x = mapRootPos.x + x * BLOCK_SIZE;
                InstantiateMapBlock(loadMap.LoadData[y][x], currentMapPos);
            }
        }
    }

    /// <summary>
    /// マップブロックの配置
    /// </summary>
    /// <param name="type">マップ種類</param>
    /// <param name="mapPos">配置座標</param>
    private void InstantiateMapBlock(MapData.LoadType type, Vector3 mapPos)
    {
        // 一旦床の生成
        GameObject.Instantiate(_FloorPref, mapPos, Quaternion.identity, _FloorRoot);

        switch (type)
        {
            case MapData.LoadType.Wall:
                GameObject.Instantiate(_WallPref, mapPos, Quaternion.identity, _WallRoot);
                break;
            case MapData.LoadType.TreeDef:
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.identity, _TreeRoot);
                break;
            case MapData.LoadType.TreeTop:
                // X+
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.Euler(90f, 0f, 0f), _TreeRoot);
                break;
            case MapData.LoadType.TreeDown:
                // x-
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.Euler(-90f, 0f, 0f), _TreeRoot);
                break;
            case MapData.LoadType.TreeRight:
                // z-
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.Euler(0f, 0f, -90f), _TreeRoot);
                break;
            case MapData.LoadType.TreeLeft:
                // z+
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.Euler(0f, 0f, 90f), _TreeRoot);
                break;

            case MapData.LoadType.Player1:
                // TODO:プレイヤー1座標の指定
                //      他のクラスから指定する可能性もあり
                break;
            case MapData.LoadType.Player2:
                // TODO:プレイヤー2座標の指定
                //      他のクラスから指定する可能性もあり
                break;

            case MapData.LoadType.Item01:
            case MapData.LoadType.Item02:
            case MapData.LoadType.Item03:
            case MapData.LoadType.Item04:
            case MapData.LoadType.Item05:
            case MapData.LoadType.Item06:
            case MapData.LoadType.Item07:
            case MapData.LoadType.Item08:
                // TODO:アイテムの配置処理
                //      アイテムの持ち方が未定なのでまだ配置処理を作らない
                break;
        }
    }
}
