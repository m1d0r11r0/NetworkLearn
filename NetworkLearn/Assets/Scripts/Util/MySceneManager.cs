using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class MySceneManager
{
    /// <summary>
    /// �g�p����V�[����
    /// (���̂܂�buildIndex�Ƃ��Ďg�p���邽�ߒǉ����͒���)
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
