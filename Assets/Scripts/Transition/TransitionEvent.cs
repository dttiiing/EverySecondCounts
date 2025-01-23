using System;
using UnityEngine;

public static class TransitionEvent
{
    public static event Action<GameSceneSO, Vector3, bool> LoadSceneRequestEvent;
    public static void CallLoadSceneRequestEvent(GameSceneSO targetScene, Vector3 cameraPos, bool isReset)
    {
        LoadSceneRequestEvent?.Invoke(targetScene, cameraPos, isReset);
    }
}
