using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public SpriteRenderer shine;
    public SpriteRenderer signImage;
    public BoxCollider2D colli;

    public PlayerState curState = PlayerState.NORMAL;
    public bool isOpen = true;

    private void Awake()
    {
        ChangeShineColor();
        shine.gameObject.SetActive(isOpen);
        colli.enabled = isOpen;
    }

    public void SwitchEmitter()
    {
        isOpen = !isOpen;
        shine.gameObject.SetActive(isOpen);
        colli.enabled = isOpen;

        Debug.Log($"{name}'s cur state is {isOpen}");
    }

    public void ChangeEmitterType()
    {
        int nextState = ((int)curState + 1) % 3;
        curState = (PlayerState)nextState;

        // todo: change color of shine
        ChangeShineColor();

        Debug.Log($"{name}'s cur emitter type is {curState}");
    }

    private void ChangeShineColor()
    {
        switch (curState)
        {
            case PlayerState.NORMAL:
                shine.color = Color.white;
                signImage.color = Color.white;
                break;

            case PlayerState.SOFT:
                shine.color = Color.red;
                signImage.color = Color.red;
                break;

            case PlayerState.HARD:
                shine.color = Color.blue;
                signImage.color = Color.blue;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // todo: change player state here
            collision.gameObject.GetComponentInParent<PlayerStateController>()?.SwitchStyle(curState);
        }
    }
}
