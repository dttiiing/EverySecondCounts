using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameSceneSO targetScene;
    public Vector3 cameraPos;
    public Vector3 resetPos;
    public bool isReset = false;

    private void Start()
    {

    }

    public void TransitionToNextScene()
    {
        TransitionEvent.CallLoadSceneRequestEvent(targetScene, cameraPos, resetPos, isReset);
    }

    public void StartNewGame()
    {
        SceneLoadManager sceneLoadManager = GameObject.Find("SceneLoadManager").GetComponent<SceneLoadManager>();
        if(sceneLoadManager != null)
        {
            sceneLoadManager.InitPLayerPos();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            TransitionToNextScene();
        }
    }
}