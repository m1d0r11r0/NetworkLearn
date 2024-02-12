using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerItemManager : NetworkBehaviour
{
    private static readonly int MAX_ITEM = 3; 
    public Queue<IUsableItem> Items = new Queue<IUsableItem>();



    /// <summary>
    /// �A�C�e���̎g�p
    /// </summary>
    /// <returns>�g�p�ł�����</returns>
    public bool UseItem(int playerId)
    {
        if (Items.TryDequeue(out var result))
        {
            result.OnUseItem(playerId, Runner);
            return true;
        }

        return false;
    }

    public bool EnqueueItem(IUsableItem item)
    {
        // �A�C�e�������Ă���ǉ����Ȃ�
        if (Items.Count >= MAX_ITEM) return false;

        Items.Enqueue(item);
        return true;
    }
}
