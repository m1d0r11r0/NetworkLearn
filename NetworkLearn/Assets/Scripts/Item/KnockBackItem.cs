using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class KnockBackItem : IUsableItem
{
    private PlayerInputManager _TargetPlayer;
    private readonly static float IMPULSE_POWER = 1.0f;

    float IUsableItem.ItemDuration => 0.5f;

    public void OnUseItem(int playerId, NetworkRunner runner)
    {
        var player = runner.GetPlayerObject(playerId == 0 ? 1 : 0);
        _TargetPlayer = player.GetBehaviour<PlayerInputManager>();
        _TargetPlayer.NetworkRb.Rigidbody.AddForce(_TargetPlayer.transform.forward * -1f * IMPULSE_POWER, ForceMode.Impulse);
        _TargetPlayer.CanMove = false;
    }
    public void OnExitItem(int playerId, NetworkRunner runner)
    {
        _TargetPlayer.CanMove = true;
    }
}
