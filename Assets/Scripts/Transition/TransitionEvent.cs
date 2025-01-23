using System;
using UnityEngine;

public static class TransitionEvent
{
    public static event Action<GameSceneSO, Vector3, Vector3, bool> LoadSceneRequestEvent;
    public static void CallLoadSceneRequestEvent(GameSceneSO targetScene, Vector3 cameraPos, Vector3 resetPos, bool isReset)
    {
        LoadSceneRequestEvent?.Invoke(targetScene, cameraPos, resetPos, isReset);
    }
}
