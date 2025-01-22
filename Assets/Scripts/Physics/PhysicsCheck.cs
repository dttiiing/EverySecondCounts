using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    ///<summary>地面图层</summary>
    private LayerMask _groundLayer;
    ///<summary>角色底部偏移量</summary>
    private Vector2 _buttomOffset;
    ///<summary>是否在地面上</summary>
    private bool _isGround;
    ///<summary>检测半径</summary>
    private float _checkRadius;

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        Check();
    }
    ///<summary>初始化物理检查组件</summary>
    private void Init()
    {
        _isGround = false;
        _checkRadius = 0.1f;
        _groundLayer = LayerMask.GetMask("Ground");
        _buttomOffset = new Vector2(-0.11f, 0);
    }
    ///<summary>执行地面检测</summary>
    private void Check()
    {
        //  监测是否在地上
        _isGround = Physics2D.OverlapCircle((Vector2)transform.position + _buttomOffset, _checkRadius, _groundLayer);
    }
    ///<summary>返回是否在地面上的状态</summary>
    public bool IsGround()
    {
        Debug.Log("IsGround:" + _isGround);
        return _isGround;
    }
    ///<summary>在选中时绘制地面碰撞检测范围</summary>
    private void OnDrawGizmosSelected()
    {
        //绘制地面碰撞检测
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(-0.11f, 0), 0.1f);
    }
}
