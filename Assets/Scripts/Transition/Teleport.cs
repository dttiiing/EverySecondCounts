using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameSceneSO targetScene;
    public Vector3 cameraPos;
    public bool isReset = false;

    private void Start()
    {

    }

    public void TransitionToNextScene()
    {
        TransitionEvent.CallLoadSceneRequestEvent(targetScene, cameraPos, isReset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            TransitionToNextScene();
        }
    }
}