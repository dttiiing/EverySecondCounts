using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public EmitterType curType = EmitterType.Normal;
    public bool isOpen = true;

    public void SwitchEmitter()
    {
        isOpen = !isOpen;

        Debug.Log($"{name}'s cur state is {isOpen}");
    }

    public void ChangeEmitterType()
    {
        int nextType = ((int)curType + 1) % 3;
        curType = (EmitterType)nextType;

        Debug.Log($"{name}'s cur emitter type is {curType}");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // todo: change player type
        }
    }
}
