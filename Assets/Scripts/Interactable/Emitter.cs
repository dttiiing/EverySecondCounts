using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public PlayerState curState = PlayerState.NORMAL;
    public bool isOpen = true;

    public void SwitchEmitter()
    {
        isOpen = !isOpen;

        Debug.Log($"{name}'s cur state is {isOpen}");
    }

    public void ChangeEmitterType()
    {
        int nextState = ((int)curState + 1) % 3;
        curState = (PlayerState)nextState;

        Debug.Log($"{name}'s cur emitter type is {curState}");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // todo: change player type
        }
    }
}
