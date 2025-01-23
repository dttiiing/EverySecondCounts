using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StyleSoft : MonoBehaviour, IPlayer
{
    private void Start()
    {
        Init();
    }

    private void Init()
    {
    }
    public PlayerState GetPlayerState() { return PlayerState.SOFT; }

    public bool IsJumpTwiceValid() { return false; }

    public bool IsBreakWallValid() { return false; }
}