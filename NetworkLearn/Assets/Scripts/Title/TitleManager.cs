using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// ボタンからゲームを開始する用
    /// </summary>
    public void MoveToGame()
    {
        MySceneManager.OpenSceneSync(MySceneManager.Scene.Game);
    }

    /// <summary>
    /// ボタンからアプリケーションを終了させる用
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
