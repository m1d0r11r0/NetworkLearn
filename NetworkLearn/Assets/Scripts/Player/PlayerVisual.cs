using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class PlayerVisual : NetworkBehaviour
{
    [SerializeField] Renderer _Renderer;
    [SerializeField] TMP_Text _NameText;
    [SerializeField] float _UIPadding;
    private Material _InstanceMat;
    private Camera _MainCam;
    private Transform _Transform;
    private RectTransform _TextTransform;

    [Networked(OnChanged = nameof(OnChangeColor))]
    public Color _Color { get; set; } = Color.gray;
    [Networked(OnChanged = nameof(OnChangeName))]
    public NetworkString<_16> _Name { get; set; } = "Player";

    private void Update()
    {
        _MainCam ??= Camera.main;
        _Transform ??= transform;
        _TextTransform ??= _NameText.rectTransform;

        _TextTransform.anchoredPosition = (Vector2)_MainCam.WorldToScreenPoint(_Transform.position) + (Vector2.up * _UIPadding);
    }

    public override void Spawned()
    {
        _InstanceMat = _Renderer.material;
        _Renderer.sharedMaterial = _InstanceMat;
        if (Object.HasInputAuthority)
        {
            Rpc_SetColor(GameStatic.LocalPlayerColor);
            Rpc_SetNickName(GameStatic.LocalPlayerName);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]

    private void Rpc_SetNickName(string name)
    {
        _Name = name;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]

    private void Rpc_SetColor(Color color)
    {
        _Color = color;
    }

    // ネットワークプロパティ（NickName）が更新された時に呼ばれるコールバック 
    public static void OnChangeColor(Changed<PlayerVisual> changed)
    {
        // 更新されたプレイヤー名をテキストに反映する
        changed.Behaviour._InstanceMat.SetColor("_BaseColor", changed.Behaviour._Color); 

    }

    // ネットワークプロパティ（NickName）が更新された時に呼ばれるコールバック 
    public static void OnChangeName(Changed<PlayerVisual> changed)
    {
        // 更新されたプレイヤー名をテキストに反映する
        changed.Behaviour._NameText.text = changed.Behaviour._Name.Value;
    }
}
