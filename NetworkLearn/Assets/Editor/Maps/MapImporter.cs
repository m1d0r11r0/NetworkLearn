//#define __DEBUG
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class MapImporter : Editor
{
    private const string GAS_URL = "https://script.google.com/macros/s/AKfycbwimdCkwz7yZfxfIuj0GKL5wCIF9RZQNw9JbIimlXSrOvwCx3MggUq65pr_-FYJ5NNU/exec";
    private const string MAP_SAVE_PATH = "Assets/Resources/Maps/{0}.asset";

    [System.Serializable]
    private struct MapRawData
    {
        public string name;
        public int size_x;
        public int size_y;
        public string csv_data;
    }
    [System.Serializable]
    private struct RequestData
    {
        public MapRawData[] map_data;
#if __DEBUG
        public void ShowLog()
        {
            string msg = "";
            foreach (var map in map_data)
            {
                msg += string.Format("name:{0}, size:({1},{2})\n", map.name, map.size_x, map.size_y);
            }
            Debug.Log(msg);
        }
#endif
    }

    [MenuItem("MyMenu/マップデータを取得")]
    private static async void GetSpreadSheetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(GAS_URL);

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            RequestData parameter = JsonUtility.FromJson<RequestData>(json);
            
            foreach(var mapData in parameter.map_data)
            {
                var assetInst = ScriptableObject.CreateInstance<MapData>();
                assetInst.MapName = mapData.name;
                assetInst.MapSize = new Vector2Int(mapData.size_x, mapData.size_y);
                string csvData = mapData.csv_data.Replace("\\n", "\n");
                assetInst.LoadData = DecodeCSVData(csvData, assetInst.MapSize, out bool result);
                if (result)
                {
                    string savePath = string.Format(MAP_SAVE_PATH, assetInst.MapName);
                    //if (File.Exists(savePath)) AssetDatabase.DeleteAsset(savePath);
                    AssetDatabase.CreateAsset(assetInst, savePath);
                }
            }
            AssetDatabase.Refresh();
#if __DEBUG
            Debug.Log(json);
            parameter.ShowLog();
#endif
        }
        else
        {
            Debug.LogError("[!Error] Jsonダウンロードエラー\n" + request.error);
        }
    }

    private static MapData.LoadTypeList[] DecodeCSVData(string csvStr, Vector2Int mapSize, out bool result)
    {
        MapData.LoadTypeList[] resData = new MapData.LoadTypeList[mapSize.y];

        string[] csvLine = csvStr.Split('\n');

        for (int y = 0; y < mapSize.y; y++)
        {
            resData[y] = new MapData.LoadTypeList(mapSize.x);
            string[] csvCell = csvLine[y].Split(',');
            for (int x = 0; x < mapSize.x; x++)
            {
                if (int.TryParse(csvCell[x], out int csvData) && Enum.IsDefined(typeof(MapData.LoadType), csvData))
                {
                    resData[y][x] = (MapData.LoadType)csvData;
                }
                else
                {
                    Debug.LogError($"スプレッドシートに不正な値が入っています。");
                    result = false;
                    return null;
                }
            }
        }

        result = true;
        return resData;
    }
}
