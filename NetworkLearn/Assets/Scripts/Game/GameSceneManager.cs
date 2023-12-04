using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームシーン管理
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    private GameStateBase _CurrentStateManager;
    private GameState _CurrentState;

    public GameState CurrentState => _CurrentState;

    private void Awake()
    {
        GameStatic.GameManager = this;
        _CurrentStateManager = null;
        _CurrentState = default(GameState);
        MoveState(GameState.Matching);
    }

    /// <summary>
    /// ステートの遷移
    /// </summary>
    /// <param name="nextState"></param>
    public void MoveState(GameState nextState)
    {
        if(_CurrentStateManager != null)
        {
            _CurrentStateManager.onExit();
        }

        switch(nextState)
        {
            case GameState.Matching:
                _CurrentStateManager = GameStatic.MatchingState;
                break;
            
            case GameState.InGame:
                _CurrentStateManager = GameStatic.InGameState;
                break;
            
            case GameState.Result:
                _CurrentStateManager = GameStatic.ResultState;
                break;

            default:
                Debug.Log("ステートが未定義です。");
                return;
        }

        _CurrentState = nextState;
        _CurrentStateManager.onInit();
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void ExitGame()
    {
        _CurrentStateManager.onExit();
        SceneManager.LoadScene(Constants.SCENE_IDX_TITLE);
    }
}
