using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerItemManager : NetworkBehaviour
{
    private static readonly int MAX_ITEM = 3; 
    public Queue<IUsableItem> Items = new Queue<IUsableItem>();



    /// <summary>
    /// アイテムの使用
    /// </summary>
    /// <returns>使用できたか</returns>
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
        // アイテムが溢れてたら追加しない
        if (Items.Count >= MAX_ITEM) return false;

        Items.Enqueue(item);
        return true;
    }
}
