using Unity.Mathematics;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    ///<summary>动画控制器</summary>
    private Animator _animator;
    ///<summary>玩家输入控制</summary>
    private PlayerInputControl _inputControl;
    ///<summary>刚体组件</summary>
    private Rigidbody2D _rigidbody;
    ///<summary>物理检查组件</summary>
    private PlayerCollision _playerCollision;
    ///<summary>角色组件</summary>
    private PlayerMovement _playerMove;

    private void Awake()
    {
        Init();
    }
    ///<summary>启用输入控制</summary>
    private void OnEnable()
    {
        _inputControl.Enable();
    }
    ///<summary>禁用输入控制</summary>
    private void OnDisable()
    {
        _inputControl.Disable();
    }
    private void Update()
    {
        SetAnimation();
    }
    ///<summary>初始化组件引用</summary>
    private void Init()
    {
        _animator = GetComponent<Animator>();
        _inputControl = new PlayerInputControl();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _playerCollision = GetComponentInParent<PlayerCollision>();
        _playerMove = GetComponentInParent<PlayerMovement>();
    }
    ///<summary>根据玩家输入和角色状态设置动画状态</summary>
    private void SetAnimation()
    {
        float moveInputX = math.abs(_inputControl.GamePlay.Move.ReadValue<Vector2>().x);
        bool isMoving = moveInputX > 0.01;
        _animator.SetBool("isMoving", isMoving);
        _animator.SetBool("isGround", _playerCollision.OnGround());
        _animator.SetFloat("velocityY", _rigidbody.velocity.y);

    }
}
