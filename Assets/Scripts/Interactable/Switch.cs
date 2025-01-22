using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public List<Emitter> emitterList;

    private bool canOperate = false;

    private void Update()
    {
        if(canOperate && Input.GetKeyDown(KeyCode.E))
        {
            TriggerAction();
        }
    }

    public void TriggerAction()
    {
        foreach (var emitter in emitterList)
        {
            emitter.SwitchEmitter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            canOperate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canOperate = false;
        }
    }
}
