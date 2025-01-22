using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameSceneSO targetScene;
    public bool isResetPlayerPos = false;
    public Vector3 targetPosition;
    public bool isPlayAnim = false;

    public void TransitionToNextScene()
    {
        TransitionEvent.CallLoadSceneRequestEvent(targetScene, isResetPlayerPos, targetPosition, isPlayAnim);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TransitionToNextScene();
        }
    }
}