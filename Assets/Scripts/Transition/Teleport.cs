using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //public SceneLoadEventSO loadEventSO;
    public GameSceneSO targetScene;
    public Vector3 targetPosition;
    public bool isPlayAnim = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //loadEventSO.CallloadRequestEvent(targetScene, targetPosition, isPlayAnim);
            TransitionEvent.CallLoadRequestEvent(targetScene, targetPosition, isPlayAnim);
        }
    }
}