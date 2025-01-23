using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateController : MonoBehaviour
{
    private GameObject[] _playerStyles;
    private IPlayer _currentPlayerForm;
    public UnityEvent onPlayerStyleChange;//用于通知形态切换
    private PlayerState _state = PlayerState.INVALID;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        _playerStyles = new GameObject[Enum.GetValues(typeof(PlayerState)).Length - 1]; // 忽略 INVALID 状态
        foreach (PlayerState state in Enum.GetValues(typeof(PlayerState)))
        {
            if (state == PlayerState.INVALID) continue;

            string styleName = state.GetObjetName();
            GameObject styleObject = GameObject.Find(styleName);
            if (styleObject != null)
                _playerStyles[(int)state] = styleObject;
            else
                Debug.LogError($"GameObject with name {styleName} not found!");
        }
        SwitchStyle(PlayerState.NORMAL);
    }

    /// <summary> 获取当前形态的 IPlayer 接口 </summary>
    public IPlayer GetPlayerStyle() { return _currentPlayerForm; }
    // <summary> 切换到指定形态 </summary>
    /// <param name="style">目标形态</param>
    public void SwitchStyle(PlayerState style)
    {
        if (style == _state) return;
        for (int i = 0; i < _playerStyles.Length; i++) _playerStyles[i].SetActive(i == (int)style);
        _state = style;
        _currentPlayerForm = _playerStyles[(int)style].GetComponent<IPlayer>();
        onPlayerStyleChange?.Invoke();
    }
}
