using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerInputManager : NetworkBehaviour
{
    [Networked]
    public int JoinedID { get; set; }

    private PlayerInput _Input;
    private Rigidbody _Rb;
    private NetworkRigidbody _NetRb;

    // Start is called before the first frame update
    void Awake()
    {
        _Input = new PlayerInput();
        _NetRb = GetComponent<NetworkRigidbody>();
        _Rb = _NetRb.Rigidbody;
    }

    public override void Spawned()
    {
        JoinedID = Object.InputAuthority;
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
    }

    private void ApplyInput(UserInputData data)
    {
        _Rb.velocity = data.Direction;
    }    
}
