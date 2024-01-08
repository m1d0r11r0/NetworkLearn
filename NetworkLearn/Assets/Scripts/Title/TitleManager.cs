using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// �{�^������Q�[�����J�n����p
    /// </summary>
    public void MoveToGame()
    {
        MySceneManager.OpenSceneSync(MySceneManager.Scene.Game);
    }

    /// <summary>
    /// �{�^������A�v���P�[�V�������I��������p
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
