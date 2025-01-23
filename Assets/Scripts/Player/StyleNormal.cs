using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StyleNormal : MonoBehaviour, IPlayer
{
    private void Start()
    {
        Init();
    }

    private void Init()
    {
    }
    public PlayerState GetPlayerState() { return PlayerState.NORMAL; }
    public bool IsJumpTwiceValid() { return true; }

    public bool IsBreakWallValid() { return false; }
}