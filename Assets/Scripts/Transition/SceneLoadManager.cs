using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public GameObject mainCamera;
    public GameObject player;
    public GameSceneSO firstScene;

    public Animation anim;
    public AnimationClip fadeInClip;
    public AnimationClip fadeOutClip;

    private GameSceneSO _curScene;

    private GameSceneSO _targetScene;
    private Vector3 _cameraPos;
    private bool _isReset = false;
    private Vector3 _resetPos;

    private bool _isLoading = false;

    private void Awake()
    {
        player.SetActive(false);

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

    public void ReloadCurScene()
    {
        OnLoadSceneRequest(_curScene, Vector3.zero, true);
    }

    private void OnLoadSceneRequest(GameSceneSO targetScene, Vector3 cameraPos, bool isReset = false)
    {
        if (_isLoading) return;

        _isLoading = true;

        _targetScene = targetScene;
        _cameraPos = cameraPos;
        _isReset = isReset;

        StartCoroutine(TransitionToScene());
    }

    private IEnumerator TransitionToScene()
    {
        if (_isReset)
        {
            anim.Play(fadeOutClip.name);
            yield return new WaitForSeconds(1.5f);
        }

        player.SetActive(false);

        // unload current scene
        if (_curScene != null)
        {
            yield return _curScene.sceneReference.UnLoadScene();
        }

        // load next scene
        if (_targetScene != null)
        {
            yield return _targetScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        }

        // move camera
        mainCamera.transform.position = _isReset ? mainCamera.transform.position : _cameraPos;

        player.SetActive(_targetScene.sceenType == SceneType.Location);

        if (_isReset)
        {
            anim.Play(fadeInClip.name);
            yield return new WaitForSeconds(1.5f);
        }

        _curScene = _targetScene;
        _isLoading = false;
    }
}