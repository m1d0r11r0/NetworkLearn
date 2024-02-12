using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using System.Linq;
//using Cysharp.Threading;

public class NetworkLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    public static bool IsNetworkEnable = true;

    [SerializeField] private NetworkRunner _NetRunnerPref;
    [SerializeField] private NetworkPrefabRef _PlayerPref;
    [SerializeField] private Canvas _ConnectUI;

    private NetworkRunner _NetRunnerInst;
    private PlayerInput _PlayerInput;
    private PlayerInput _Input;
    private bool _IsPause = false;
    public PlayerInput Input => _Input;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    private async void Init()
    {
        if (!IsNetworkEnable) return;
        _ConnectUI.enabled = true;

        _NetRunnerInst = Instantiate(_NetRunnerPref);
        _NetRunnerInst.AddCallbacks(this);
        
        _Input = new PlayerInput();
        ConnectInput(_Input);

        var sessionParams = new Dictionary<string, SessionProperty>
        {
            { "ID", GameStatic.SessionID }
        };

        SceneRef scene = (SceneManager.GetActiveScene().buildIndex);
        var result = await _NetRunnerInst.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            SceneManager = _NetRunnerInst.GetComponent<NetworkSceneManagerDefault>(),
            SessionProperties = sessionParams, 
            PlayerCount = 2,
            Scene = scene,
        });

        if (result.Ok)
        {
            Debug.Log("Connected Game!");
            _ConnectUI.enabled = false;

        }
        else
        {
            Debug.LogAssertion(result.ErrorMessage);
        }
    }

    public void ConnectInput<T>(T input) where T : PlayerInput
    {
        _PlayerInput = input;
    }


    #region INetworkRunnerCallbacks

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // ホスト（サーバー兼クライアント）かどうかはIsServerで判定できる
        if (!runner.IsServer) { return; }

        // ランダムな生成位置（半径5の円の内部）を取得する
        var randomValue = UnityEngine.Random.insideUnitCircle * 5f;
        var spawnPosition = new Vector3(randomValue.x, 0f, randomValue.y);

        // 参加したプレイヤーのアバターを生成する
        var avatar = runner.Spawn(_PlayerPref, spawnPosition, Quaternion.identity, player);

        var inst = avatar.GetComponent<PlayerInputManager>();
        inst.PlayerId = player;

        // プレイヤー（PlayerRef）とアバター（NetworkObject）を関連付ける
        runner.SetPlayerObject(player, avatar);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) 
    {
        if (!runner.IsServer) return;

        if (runner.TryGetPlayerObject(player, out var avatar))
        {
            runner.Despawn(avatar);
            _IsPause = true;
            MyDialog dialog = new();
            dialog.ShowDialog(
                MyDialog.Type.Ok,
                "接続が切断されました。\nタイトルに戻ります。",
                (_) => {
                    runner.Shutdown(
                        destroyGameObject: true,
                        shutdownReason: ShutdownReason.Error
                        );
                    _IsPause = false;
                    MySceneManager.OpenSceneSync(MySceneManager.Scene.Title);
                });
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (_IsPause) return;
        if (_PlayerInput == null)
        {
            Debug.LogError("_PlayerInput is null");
            return;
        }
        var data = _PlayerInput.UpdateInput();
        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        if (_IsPause) return;
        MyDialog dialog = new();
        dialog.ShowDialog(
            MyDialog.Type.Ok,
            "ホストと接続が切断されました。\nタイトルに戻ります。",
            (_) => {
                MySceneManager.OpenSceneSync(MySceneManager.Scene.Title);
            });
    }

    public void OnConnectedToServer(NetworkRunner runner) { }

    public void OnDisconnectedFromServer(NetworkRunner runner) 
    {
        Debug.Log("OnDisconnectedFromServer");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("OnConnectFailed");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }
    #endregion
}
