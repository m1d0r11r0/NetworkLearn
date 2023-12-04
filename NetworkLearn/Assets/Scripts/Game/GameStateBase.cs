using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateBase
{
    public delegate void OnInit();
    public delegate void OnExit();
    public OnInit onInit;
    public OnExit onExit;
}
