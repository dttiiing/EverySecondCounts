using System;
using UnityEngine;

public static class TransitionEvent
{
    public static event Action<GameSceneSO, bool, Vector3, bool> LoadSceneRequestEvent;
    public static void CallLoadSceneRequestEvent(GameSceneSO targetScene, bool isResetPLayerPos, Vector3 targetPos, bool isPlayAnim)
    {
        LoadSceneRequestEvent?.Invoke(targetScene, isResetPLayerPos, targetPos, isPlayAnim);
    }
}
