using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ステージ選択用UI
/// </summary>
public class MatchingUI : MonoBehaviour
{
    [SerializeField] Button readyButton;
    [SerializeField] Button unreadyButton;
    [SerializeField] StateManager stateMng;
    [SerializeField] Image[] selectButtons;
    [SerializeField] TMP_Text readyText;
    [SerializeField] GameObject matchingObject;

    static readonly Color UNSELECT_COLOR = Color.clear;
    static readonly Color SELECTED_COLOR = Color.cyan;

    public void Start()
    {
        StateManager.onUpdateState += OnUpdateState;
        readyButton.interactable = false;
    }

    public void OnDestroy()
    {
        StateManager.onUpdateState -= OnUpdateState;
    }

    private void Update()
    {
        if (stateMng.Object == null) return;
        int selected = stateMng.SelectedStage;
        for (int i = 0; i < selectButtons.Length; i++)
        {
            selectButtons[i].color = (i == selected) ? SELECTED_COLOR : UNSELECT_COLOR;
        }
        readyText.text = $"{stateMng.ReadyCount}/2";
    }

    private void OnUpdateState(NetworkState state)
    {
        switch (state)
        {
            case NetworkState.SelectStage:
                readyButton.interactable = true;
                break;
            case NetworkState.Game:
                gameObject.SetActive(false);
                matchingObject.SetActive(false);
                break;
        }
        
    }

    public void BackToTitle()
    {
        MySceneManager.OpenSceneSync(MySceneManager.Scene.Title);
    }

    public void StartGame()
    {
        readyButton.gameObject.SetActive(false);
        unreadyButton.gameObject.SetActive(true);
        stateMng.Rpc_ReadyGame();
    }

    public void UnreadyGame()
    {
        stateMng.Rpc_UnreadyGame();
        readyButton.gameObject.SetActive(true);
        unreadyButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 指定したステージを開く
    /// </summary>
    /// <param name="index"></param>
    public void OpenMap(int index)
    {
        stateMng.SetStage(index);
    }
}
