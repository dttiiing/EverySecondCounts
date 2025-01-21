using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public GameSceneSO firstLoadScene;

    private void Awake()
    {
        Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        TransitionEvent.LoadRequestEvent += OnLoadRequest;
    }

    private void OnDisable()
    {
        TransitionEvent.LoadRequestEvent -= OnLoadRequest;
    }

    private void OnLoadRequest(GameSceneSO targetScene, Vector3 targetPos, bool isPlayAnim)
    {
        throw new NotImplementedException();
    }
}
