using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapLoader Loader { private set; get; }


    void Awake()
    {
        Loader = GetComponent<MapLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
