using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpeedUpItem : IUsableItem
{
    private PlayerInputManager _UsePlayer;
    float IUsableItem.ItemDuration => 5f;

    public void OnUseItem(int playerId, NetworkRunner runner)
    {
        var player = runner.GetPlayerObject(playerId);
        _UsePlayer = player.GetBehaviour<PlayerInputManager>();
        _UsePlayer.MoveSpeed = 5f;
    }
    public void OnExitItem(int playerId, NetworkRunner runner)
    {
        _UsePlayer.MoveSpeed = 1f;
    }
}
