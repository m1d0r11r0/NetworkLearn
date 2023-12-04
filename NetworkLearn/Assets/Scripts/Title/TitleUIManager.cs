using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルUI管理
/// </summary>
public class TitleUIManager : MonoBehaviour
{
    
    /// <summary>
    /// 「ゲーム開始」ボタンをクリックした際の挙動
    /// </summary>
    public void OnClickGameButton()
    {
        // ゲームシーンに移動
        SceneManager.LoadScene(Constants.SCENE_IDX_GAME);
    }
}
