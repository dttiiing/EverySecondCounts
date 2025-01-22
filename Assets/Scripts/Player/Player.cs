using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum PlayerStatus { soft, normal, hard }

public class Player : MonoBehaviour
{
    ///<summary>玩家所处状态</summary>
    [SerializeField] private int _status;

    /// <summary> 死亡事件 </summary>
    public UnityEvent OnDead;

    private void Start()
    {
        Init();
    }

    ///<summary>初始化角色属性</summary>
    private void Init()
    {
        _status = (int)PlayerStatus.normal;
    }

    ///<summary>玩家死亡</summary>
    public void Dead()
    {
        OnDead?.Invoke();
    }
}
