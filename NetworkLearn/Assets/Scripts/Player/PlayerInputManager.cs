using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerInputManager : NetworkBehaviour
{
    private static readonly float ROTATE_SPEED = 0.1f;

    [Networked]
    public float MoveSpeed { get; set; } = 1f;
    [Networked] 
    public TickTimer ItemTimer { get; set; }
    [Networked]
    public bool CanMove { get; set; } = true;


    public int PlayerId;

    private PlayerInput _Input;
    private Rigidbody _Rb;
    private NetworkRigidbody _NetRb;
    private PlayerItemManager _ItemMng;
    private IUsableItem _UsingItem;

    public NetworkRigidbody NetworkRb => _NetRb;

    // Start is called before the first frame update
    void Awake()
    {
        _Input = new PlayerInput();
        _NetRb = GetComponent<NetworkRigidbody>();
        _ItemMng = GetComponent<PlayerItemManager>();
        _Rb = _NetRb.Rigidbody;
        _Rb.maxAngularVelocity = 0f;
    }

    public override void Spawned()
    {
    }

    void FixedUpdate()
    {
        if (NetworkLauncher.IsNetworkEnable) return;
        var data = _Input.UpdateInput();
        ApplyInput(data);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out UserInputData data))
        {
            ApplyInput(data);
        }
        if (ItemTimer.Expired(Runner))
        {
            _UsingItem.OnExitItem(PlayerId, Runner);
        }
    }

    public void GameClear()
    {
        StateManager.Instance.CurrentState = NetworkState.Result;
        StateManager.Instance.WinPlayerId = PlayerId;
    }

    private void ApplyInput(UserInputData data)
    {
        // リザルト画面では入力されても適用しない
        if (StateManager.Instance.CurrentState == NetworkState.Result) return;
        Move(data.Direction);
    }

    private void Move(Vector3 direction)
    {
        if (!CanMove || direction.sqrMagnitude <= 0.01f) return;
        _Rb.velocity = direction * MoveSpeed;
        var lookRotation = Quaternion.LookRotation(direction);
        var currentRotation = Quaternion.Slerp(_Rb.rotation, lookRotation, ROTATE_SPEED);
        _Rb.MoveRotation(currentRotation);
    }

    private void OnGetItem(IUsableItem item)
    {
        _UsingItem = item;
        _ItemMng.EnqueueItem(item);
        _ItemMng.UseItem(PlayerId);
        ItemTimer = TickTimer.CreateFromSeconds(Runner, _UsingItem.ItemDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemView>(out var item))
        {
            OnGetItem(item.Item);
        }
    }
}
