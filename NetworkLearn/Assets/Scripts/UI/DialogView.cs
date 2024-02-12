using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogView : MonoBehaviour
{
    [System.Serializable]
    public class ButtonInfo
    {
        public MyDialog.Type Type;
        public GameObject Root;
        public Button OnCancelButton;
        public Button OnAcceptButton;
    }
    [SerializeField] List<ButtonInfo> _TypeButtonRoots;
    [SerializeField] TMP_Text _DescriptionTxt;

    public TMP_Text DescriptionTxt => _DescriptionTxt;

    public ButtonInfo GetDialogByType(MyDialog.Type type)
    {
        var button = _TypeButtonRoots.Find((a) => a.Type == type);
        return button;
    }
}

public class MyDialog
{
    public enum Type
    {
        Ok,
        YesNo,
    }

    public static GameObject DialogPref;

    private Type _DialogType;
    private string _Description;
    private DialogView _DialogInst;

    private System.Action<bool> _OnExit;

    public MyDialog()
    {
        Init();
    }

    public void Init()
    {
        if (DialogPref == null)
        {
            DialogPref = Resources.Load<GameObject>("Prefabs/UIUtil/DialogUI");
        }
    }

    public void ShowDialog(Type dialogType, string desctiption, System.Action<bool> onExit)
    {
        _DialogType = dialogType;
        _DialogInst = GameObject.Instantiate(DialogPref).GetComponent<DialogView>();

        var button = _DialogInst.GetDialogByType(dialogType);
        button.Root.SetActive(true);
        button.OnAcceptButton.onClick.AddListener(OnClickAccept);
        button.OnCancelButton.onClick.AddListener(OnClickCancel);

        _DialogInst.DescriptionTxt.text = desctiption;
        _OnExit = onExit;
    }

    public void OnClickCancel()
    {
        if (_OnExit != null) _OnExit.Invoke(false);
        GameObject.Destroy(_DialogInst.gameObject);
    }

    public void OnClickAccept()
    {
        if (_OnExit != null) _OnExit.Invoke(true);
        GameObject.Destroy(_DialogInst.gameObject);
    }
}
