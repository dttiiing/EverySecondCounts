using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    ///<summary>地面图层</summary>
    private LayerMask _groundLayer;
    ///<summary>是否在地面上</summary>
    private bool _isGround;
    ///<summary>检测半径</summary>
    private float _checkRadius;
    ///<summary>给碰撞组件描边选择颜色</summary>
    public Color gizmoColor = Color.red;

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
    }
    ///<summary>执行地面检测</summary>
    private void Check()
    {
        //  监测是否在地上
        _isGround = Physics2D.OverlapCircle((Vector2)transform.position, _checkRadius, _groundLayer);
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
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere((Vector2)transform.position, 0.1f);
    }
}
