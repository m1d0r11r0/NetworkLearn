using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;


/// <summary>
/// ゲームのステート
/// </summary>
public enum NetworkState
{
    Matching,
    SelectStage,
    Game,
    Result,

}

public class StateManager : NetworkBehaviour
{
    [Networked]
    public int SelectedStage { get; set; }
    [Networked(OnChanged = nameof(UpdateState))]
    public NetworkState CurrentState { get; set; }
    [Networked]
    public int ReadyCount { get; set; }

    public int WinPlayerId;

    // 疑似シングルトン
    private static StateManager _Instance;
    public static StateManager Instance => _Instance;

    public delegate void OnUpdateState(NetworkState newState);
    public static OnUpdateState onUpdateState;

    public override void Spawned()
    {
        base.Spawned();
        _Instance = this;
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        if (!Object.HasStateAuthority) return;

        switch (CurrentState)
        {
            case NetworkState.Matching:
                OnUpdateMatching();
                break;

            case NetworkState.SelectStage:
                OnUpdateSelectStage();
                break;

            case NetworkState.Game:
                OnUpdateGame();
                break;

            case NetworkState.Result:
                OnUpdateResult();
                break;
        }
    }

    protected void OnUpdateMatching()
    {
        // 現状で2人制限をかけると何かと厄介なので、コンパイルオプションで消す
#if false
        if (Runner.SessionInfo.PlayerCount == Runner.SessionInfo.MaxPlayers)
        {
            CurrentState = NetworkState.SelectStage;
        }
#else
        if (Input.GetKeyDown(KeyCode.Space)) CurrentState = NetworkState.SelectStage;
#endif
    }

    protected void OnUpdateSelectStage()
    {
    }

    protected void OnUpdateGame() { }

    protected void OnUpdateResult() { }

    private static void UpdateState(Changed<StateManager> changed)
    {
        onUpdateState?.Invoke(changed.Behaviour.CurrentState);
    }


    public void SetStage(int stageId)
    {
        Rpc_SetStage(stageId);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Rpc_SetStage(int stageId)
    {
        SelectedStage = stageId;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Rpc_ReadyGame()
    {
        ReadyCount++;
#if false
        if (ReadyCount == Runner.SessionInfo.MaxPlayers)
#else
        if (ReadyCount == 1)
#endif
        {
            CurrentState = NetworkState.Game;
            Rpc_BeginGame();
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_BeginGame()
    {
        foreach (var player in Runner.ActivePlayers)
        {
            if (Runner.TryGetPlayerObject(player, out var netObj))
            {
                var inst = netObj.GetComponent<NetworkRigidbody>();
                if (inst != null)
                {
                    MapManager.Loader._Players[player] = inst;
                }    
            }
        }
        MapManager.Loader.LoadMap(SelectedStage);
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Rpc_UnreadyGame()
    {
        ReadyCount--;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_GameOver()
    {
        ReadyCount--;
    }
}
