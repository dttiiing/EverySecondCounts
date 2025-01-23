using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StyleHar : MonoBehaviour, IPlayer
{
    private void Start()
    {
        Init();
    }

    private void Init()
    {
    }
    public int playerState() { return (int)PlayerState.HARD; }

    public bool IsJumpTwiceValid() { return false; }

    public bool IsBreakWallValid() { return true; }
}