using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettingUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _PlNameInput;

    [SerializeField] private Slider _RSlider;
    [SerializeField] private Slider _GSlider;
    [SerializeField] private Slider _BSlider;
    [SerializeField] private Image _ColorView;

    private void Start()
    {
        _PlNameInput.name = GameStatic.LocalPlayerName;
        _RSlider.value = GameStatic.LocalPlayerColor.r;
        _GSlider.value = GameStatic.LocalPlayerColor.g;
        _BSlider.value = GameStatic.LocalPlayerColor.b;
        _ColorView.color = GameStatic.LocalPlayerColor;
    }

    public void SetSlider(float value) => SetColor();
    private void SetColor()
    {
        Color color = new Color(_RSlider.value, _GSlider.value, _BSlider.value);
        GameStatic.LocalPlayerColor = color;
        _ColorView.color = color;
    }

    public void SetPlayerName(string name)
    {
        GameStatic.LocalPlayerName = name;
    }
}
