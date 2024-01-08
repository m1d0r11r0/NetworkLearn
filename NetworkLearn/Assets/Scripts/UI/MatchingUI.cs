using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�I��pUI
/// </summary>
public class MatchingUI : MonoBehaviour
{
    public void BackToTitle()
    {
        MySceneManager.OpenSceneSync(MySceneManager.Scene.Title);
    }

    /// <summary>
    /// �w�肵���X�e�[�W���J��
    /// </summary>
    /// <param name="index"></param>
    public void OpenMap(int index)
    {
        if (!MapManager.Loader)
        {
            Debug.LogError("MapLoader��������܂���B");
        }
        MapManager.Loader?.LoadMap(index);

        // �������̃I�u�W�F�N�g���̕s�v�Ȃ̂ō폜
        Destroy(gameObject);
    }
}
