using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g��UI�Ǘ�
/// </summary>
public class TitleUIManager : MonoBehaviour
{
    
    /// <summary>
    /// �u�Q�[���J�n�v�{�^�����N���b�N�����ۂ̋���
    /// </summary>
    public void OnClickGameButton()
    {
        // �Q�[���V�[���Ɉړ�
        SceneManager.LoadScene(Constants.SCENE_IDX_GAME);
    }
}
