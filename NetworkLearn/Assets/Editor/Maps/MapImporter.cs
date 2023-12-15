#define __DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class MapImporter : Editor
{
    private const string GAS_URL = "https://script.google.com/macros/s/AKfycbwimdCkwz7yZfxfIuj0GKL5wCIF9RZQNw9JbIimlXSrOvwCx3MggUq65pr_-FYJ5NNU/exec";

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
            // ここでparameterを使って処理を行う
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
}
