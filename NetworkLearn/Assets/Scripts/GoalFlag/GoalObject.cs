using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GoalObject : MonoBehaviour
{
    // ゴール用マテリアルのα
    private static readonly float GOAL_ALPHA = 0.5f;

    private Material _GoalMat;

    private Color _GoalColor;
    private int _GoalPlayerId;
    private bool _IsInitialized = false;

    public int TargetPlayerId
    {
        get { return _GoalPlayerId; }
        set { _GoalPlayerId = value; }
    }

    public void SetColor(Color color)
    {
        _GoalColor = color;
        _GoalColor.a = GOAL_ALPHA;
        _GoalMat.SetColor("_BaseColor", _GoalColor);
    }

    private void Awake()
    {
        // MeshRendererからマテリアルのインスタンスを取得
        var renderer = GetComponentInChildren<Renderer>();
        _GoalMat = renderer.material;
        renderer.sharedMaterial = _GoalMat;
    }

    public async UniTask Initialize(int playerId, Color color)
    {
        SetColor(color);
        _GoalPlayerId = playerId;

        // 1フレーム待機（プレイヤーのワープ待ち）
        await UniTask.NextFrame();

        _IsInitialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_IsInitialized) return;

        var player = other.GetComponentInParent<PlayerInputManager>();
        if (player)
        {
            if (_GoalPlayerId == player.PlayerId)
            {
                player.GameClear();
            }
        }
    }
}
