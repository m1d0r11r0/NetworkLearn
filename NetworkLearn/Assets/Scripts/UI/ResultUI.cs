using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject _OnWinUI;
    [SerializeField] private GameObject _OnLoseUI;

    // Start is called before the first frame update
    void Start()
    {
        StateManager.onUpdateState += OnUpdateState;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StateManager.onUpdateState -= OnUpdateState;
    }

    private void OnUpdateState(NetworkState state)
    {
        switch (state)
        {
            case NetworkState.Result:
                gameObject.SetActive(true);
                var local = StateManager.Instance.Runner.LocalPlayer;
                var win = StateManager.Instance.WinPlayerId;
                SetResultUI(local, win);
                break;
        }
    }

    public void SetResultUI(int localPlId, int winPlId)
    {
        if (localPlId == winPlId)
        {
            _OnWinUI.SetActive(true);
        }
        else
        {
            _OnLoseUI.SetActive(true);
        }
    }
}
