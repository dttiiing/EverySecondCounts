using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public List<GameObject> playerStateList;

    private int curState;

    private void Awake()
    {
        curState = 0;
        ChangePlayerState(curState);
    }

    public void ChangePlayerState(int nextState)
    {
        Vector3 curpos = playerStateList[curState].transform.position;

        for (int i = 0; i < playerStateList.Count; i++)
        {
            playerStateList[i].SetActive(i == nextState);
        }

        playerStateList[nextState].transform.position = curpos;
        curState = nextState;
    }
}