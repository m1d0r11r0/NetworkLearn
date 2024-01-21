using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class ConnectionUI : MonoBehaviour
{
    [SerializeField] TMP_Text _Text;
    [SerializeField] int _DelayMs;

    // Start is called before the first frame update
    void Start()
    {
        // CancellationToken�̎擾
        var ct = this.GetCancellationTokenOnDestroy();
        ConnectionTxtAnim(ct).Forget();
    }

    private async UniTask ConnectionTxtAnim(CancellationToken token)
    {
        const int DEFAULT_VISIBLE_CHAR = 10;
        const int MAX_VISIBLE_CHAR = 13;
        // �����Ƀ��[�v��������
        while (true)
        {
            // (0,0,5)�Ɉړ�����̂�҂�
            await UniTask.Delay(_DelayMs);
            _Text.maxVisibleCharacters++;

            if (_Text.maxVisibleCharacters > MAX_VISIBLE_CHAR)
            {
                _Text.maxVisibleCharacters = DEFAULT_VISIBLE_CHAR;
            }
        }
    }
}
