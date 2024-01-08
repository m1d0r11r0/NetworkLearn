using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class MySceneManager
{
    /// <summary>
    /// 使用するシーン列挙
    /// (そのままbuildIndexとして使用するため追加時は注意)
    /// </summary>
    public enum Scene
    {
        Title = 0,
        Game
    }

    public static void OpenSceneSync(Scene nextScene, LoadSceneMode mode = default)
    {
        SceneManager.LoadScene((int)nextScene, mode);
    }

    public static void OpenSceneAsync(Scene nextScene, LoadSceneMode mode = default)
    {
        SceneManager.LoadSceneAsync((int)nextScene, mode);
    }
}
