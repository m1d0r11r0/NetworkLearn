using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[System.Flags]
public enum InputButton
{
    A = 1 << 1,
    B = 1 << 2,
    X = 1 << 3,
    Y = 1 << 4,
}

public struct UserInputData : INetworkInput
{
    public Vector3 Direction;
    public InputButton UserButton;
}

public class PlayerInput
{

    private UserInputData _InputData;

    public UserInputData InputData => _InputData;

    public virtual UserInputData UpdateInput()
    {
        var direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            direction += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            direction += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right;

        _InputData.Direction = direction.normalized;

        return _InputData;
    }
}
