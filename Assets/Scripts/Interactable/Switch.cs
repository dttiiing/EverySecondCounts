using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public GameObject handleLeft;
    public GameObject handleRight;
    public List<Emitter> emitterList;

    private bool canOperate = false;
    private bool isOn = false;

    private void Update()
    {
        if (canOperate && Input.GetKeyDown(KeyCode.E))
        {
            SwitchHandleState();
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

    private void SwitchHandleState()
    {
        isOn = !isOn;

        handleLeft.SetActive(!isOn);
        handleRight.SetActive(isOn);
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
