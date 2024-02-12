using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public abstract void OnUseItem(PlayerInputManager item);
    public abstract void OnGetItem(PlayerInputManager item);
}
