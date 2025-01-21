using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> loadRequestEvent;
    public void CallloadRequestEvent(GameSceneSO targetScene, Vector3 targetPos, bool isPlayAnim)
    {
        loadRequestEvent?.Invoke(targetScene, targetPos, isPlayAnim);
    }
}