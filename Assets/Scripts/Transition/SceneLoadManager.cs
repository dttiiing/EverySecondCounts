using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject player;
    public GameSceneSO firstScene;
    //public AnimationClip loadSceneAnim;
    //public AnimationClip unloadSceneAnim;

    private GameSceneSO _curScene;

    private GameSceneSO _targetScene;
    private bool _isResetPlayerPos = false;
    private Vector3 _targetPos;
    private bool _isPlayAnim = false;

    private bool _isLoading = false;

    private void Awake()
    {
        _curScene = firstScene;
        _curScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
    }

    private void OnEnable()
    {
        TransitionEvent.LoadSceneRequestEvent += OnLoadSceneRequest;
    }

    private void OnDisable()
    {
        TransitionEvent.LoadSceneRequestEvent -= OnLoadSceneRequest;
    }

    private void OnLoadSceneRequest(GameSceneSO targetScene, bool isResetPlayerPos, Vector3 targetPos, bool isPlayAnim)
    {
        if (_isLoading) return;

        _isLoading = true;

        _targetScene = targetScene;
        _isResetPlayerPos = isResetPlayerPos;
        _targetPos = targetPos;
        _isPlayAnim = isPlayAnim;

        StartCoroutine(TransitionToScene());
    }

    private IEnumerator TransitionToScene()
    {
        if (_isPlayAnim)
        {
            // todo: play unload anim
        }

        if (_curScene != null)
        {
            yield return _curScene.sceneReference.UnLoadScene();
            player.SetActive(false);
        }

        if (_targetScene != null)
        {
            var loadOperation = _targetScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            loadOperation.Completed += OnLoadSceneCompleted;

        }
    }

    private void OnLoadSceneCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        _curScene = _targetScene;

        if (_isResetPlayerPos)
        {
            player.transform.position = _targetPos;
        }
        player.SetActive(true);

        if (_isPlayAnim)
        {
            // todo: play load anim
        }

        _isLoading = false;
    }

    
}
