using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public interface IUsableItem
{
    public void OnUseItem(int usePlayerId, NetworkRunner runner);
    public void OnExitItem(int usePlayerId, NetworkRunner runner);
    public float ItemDuration { get; }
    //public void OnGetItem(int playerId);
}

public static class ItemStatic
{
    public enum Type
    {
        SpeedUp,
        KnockBack,
    }
    public static IReadOnlyDictionary<Type, System.Type> Items = new Dictionary<Type, System.Type>()
    {
        { Type.SpeedUp, typeof(SpeedUpItem) },
        { Type.KnockBack, typeof(KnockBackItem) },

    };

    public static IUsableItem GetItem(Type type)
    {
        return Activator.CreateInstance(Items[type]) as IUsableItem;
    }
}