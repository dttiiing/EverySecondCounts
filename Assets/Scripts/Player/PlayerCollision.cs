using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollision : MonoBehaviour
{
    ///<summary>地面图层</summary>
    private LayerMask _groundLayer;
    ///<summary>可破坏图层编号</summary>
    private int _weakLayer;
    ///<summary>玩家状态控制器</summary>
    private PlayerStateController _playerStateController;
    ///<summary>当前玩家形态</summary>
    private IPlayer _currentPlayerForm;
    ///<summary>检测半径</summary>
    public float checkRadius = 0.1f;

    private void Awake()
    {
        Init();
    }
    ///<summary>初始化物理检查组件</summary>
    private void Init()
    {
        _groundLayer = LayerMask.GetMask("Ground");
        _weakLayer = LayerMask.NameToLayer("WeakGround");
        _playerStateController = GetComponent<PlayerStateController>();
        _playerStateController.onPlayerStyleChange.AddListener(() => { _currentPlayerForm = _playerStateController.GetPlayerStyle(); });
    }
    ///<summary>返回是否在地面上</summary>
    public bool OnGround()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position, checkRadius, _groundLayer);
    }
    ///<summary>在选中时绘制地面碰撞检测范围</summary>
    private void OnDrawGizmosSelected()
    {
        //绘制地面碰撞检测
        Gizmos.color = Color.red; ;
        Gizmos.DrawWireSphere((Vector2)transform.position, checkRadius);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            _playerStateController.SwitchStyle(PlayerState.DEAD);
        }
    }
}
