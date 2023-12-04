using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatic
{
    public static GameSceneManager GameManager;

    public static GameStateBase MatchingState = new GameStateBase();
    public static GameStateBase InGameState = new GameStateBase();
    public static GameStateBase ResultState = new GameStateBase();
}

public enum GameState
{
    Matching,
    InGame,
    Result
}