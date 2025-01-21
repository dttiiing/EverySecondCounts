using System;
using UnityEngine;

public static class TransitionEvent
{
    public static event Action<GameSceneSO, Vector3, bool> LoadRequestEvent;
    public static void CallLoadRequestEvent(GameSceneSO targetScene, Vector3 targetPos, bool isPlayAnim)
    {
        LoadRequestEvent?.Invoke(targetScene, targetPos, isPlayAnim);
    }
}
