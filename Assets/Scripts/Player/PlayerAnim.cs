using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    ///<summary>动画控制器</summary>
    private Animator _animator;
    ///<summary>玩家输入控制</summary>
    private PlayerInputControl _inputControl;
    ///<summary>刚体组件</summary>
    private Rigidbody2D _rigidbody;
    ///<summary>物理检查组件</summary>
    private PhysicsCheck _physicsCheck;
    ///<summary>角色组件</summary>
    private Player _player;
    ///<summary>玩家控制组件</summary>
    private PlayerController _playerController;

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
        _rigidbody = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        _player = GetComponent<Player>();
        _playerController = GetComponent<PlayerController>();
    }
    ///<summary>根据玩家输入和角色状态设置动画状态</summary>
    private void SetAnimation()
    {
        float moveInputX = math.abs(_inputControl.GamePlay.Move.ReadValue<Vector2>().x);
        bool isRunning = moveInputX > 0.7 ? true : false;
        bool isWalking = moveInputX > 0.01 ? true : false;
        _animator.SetBool("isRunning", isRunning);
        _animator.SetBool("isWalking", isWalking);
        _animator.SetBool("isDead", _playerController.isDead);
        _animator.SetBool("isGround", _physicsCheck.IsGround());
        _animator.SetFloat("velocityY", _rigidbody.velocity.y);

    }
    ///<summary>播放玩家受伤动画</summary>
    public void PlayerInjured()
    {
        _animator.SetTrigger("injured");
    }
}
