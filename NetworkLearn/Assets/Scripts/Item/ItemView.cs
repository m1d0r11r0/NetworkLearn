using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [SerializeField] ItemStatic.Type _ItemType;

    public IUsableItem Item { private set; get; }

    private void Awake()
    {
        Item = ItemStatic.GetItem(_ItemType);
    }
}
