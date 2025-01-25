using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;
    public GameSceneSO firstScene;

    public FadeController fadeController;

    private GameSceneSO _curScene;
    private GameSceneSO _targetScene;
    private Vector3 _cameraPos;
    private bool _isLoading = false;

    private bool _isReset = false;
    private Vector3 _resetPos;
    private PlayerState _resetState;

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
        fadeController.PlayFadeAnim();
    }

    public void DoReload()
    {
        OnLoadSceneRequest(_curScene, Vector3.zero, _resetPos, true);
    }

    public void InitPLayerPos()
    {
        player.transform.position = new Vector3(-8, 6, 0);
    }

    private void OnLoadSceneRequest(GameSceneSO targetScene, Vector3 cameraPos, Vector3 resetPos, bool isReset = false)
    {
        if (_isLoading) return;

        _isLoading = true;

        _targetScene = targetScene;
        _cameraPos = cameraPos;
        _resetPos = resetPos;
        _isReset = isReset;
        player.SetActive(false);

        StartCoroutine(TransitionToScene());
    }

    private IEnumerator TransitionToScene()
    {
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

        if (_isReset)
        {
            player.transform.position = _resetPos;
        }
        player.SetActive(_targetScene.sceenType == SceneType.Location);

        _curScene = _targetScene;
        _isLoading = false;
    }
}