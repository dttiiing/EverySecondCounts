using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public GameObject handleImage;
    public List<Emitter> emitterList;

    private bool canOperate = false;

    private void Update()
    {
        if (canOperate && Input.GetKeyDown(KeyCode.E))
        {
            Anim();
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

    private void Anim()
    {
        Vector3 curDir = handleImage.transform.localScale;
        handleImage.transform.localScale = new Vector3(curDir.x * -1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOperate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOperate = false;
        }
    }
}
