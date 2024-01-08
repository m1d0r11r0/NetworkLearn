using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ選択用UI
/// </summary>
public class MatchingUI : MonoBehaviour
{
    public void BackToTitle()
    {
        MySceneManager.OpenSceneSync(MySceneManager.Scene.Title);
    }

    /// <summary>
    /// 指定したステージを開く
    /// </summary>
    /// <param name="index"></param>
    public void OpenMap(int index)
    {
        if (!MapManager.Loader)
        {
            Debug.LogError("MapLoaderが見つかりません。");
        }
        MapManager.Loader?.LoadMap(index);

        // もうこのオブジェクト自体不要なので削除
        Destroy(gameObject);
    }
}
