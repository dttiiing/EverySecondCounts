using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    ///<summary>玩家键盘/控制器输入控制</summary>
    public PlayerInputControl inputControl;
    ///<summary>角色刚体</summary>
    public Rigidbody2D _rigidbody;
    ///<summary>玩家输入方向</summary>
    private Vector2 _inputDirection;
    ///<summary>移动速度</summary>
    [SerializeField] private float _speed;
    ///<summary>跳跃力量</summary>
    [SerializeField] private float _jumpForce;
    ///<summary>物理检查组件</summary>
    private PlayerCollision _playerCollision;
    ///<summary>玩家状态控制器</summary>
    private PlayerStateController _playerStateController;
    ///<summary>当前玩家形态</summary>
    private IPlayer _currentPlayerForm;
    ///<summary>是否第一次跳跃</summary>
    private bool _isFirstJump = false;

    private void Awake()
    {
        Init();
        // +=用于绑定Jump方法到Jump输入动作的事件监听
        inputControl.GamePlay.Jump.started += Jump;
    }

    ///<summary>启用输入控制</summary>
    private void OnEnable()
    {
        inputControl.Enable();
    }

    ///<summary>禁用输入控制</summary>
    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        _inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    ///<summary>固定更新，处理玩家移动逻辑</summary>
    private void FixedUpdate()
    {
        PlayerMove();
    }

    ///<summary>初始化角色属性和组件</summary>
    private void Init()
    {
        _speed = 270;
        _jumpForce = 16.5f;
        _rigidbody = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();
        _playerCollision = GetComponent<PlayerCollision>();
        _playerStateController = GetComponent<PlayerStateController>();
        _playerStateController.onPlayerStyleChange.AddListener(() => { _currentPlayerForm = _playerStateController.GetPlayerStyle(); });
    }
    ///<summary>控制玩家移动</summary>
    public void PlayerMove()
    {
        // 根据输入方向计算速度，保持与帧率无关的平稳移动
        _rigidbody.velocity = new Vector2(_inputDirection.x * _speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
        PlayerTransForm(_inputDirection.x);
    }

    private void PlayerTransForm(float _tmpdir)
    {
        // 根据输入方向调整角色朝向
        if (_tmpdir < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_tmpdir > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    ///<summary>处理玩家跳跃</summary>
    private void Jump(InputAction.CallbackContext obj)
    {
        // 当在地面上时执行跳跃
        if (_playerCollision.OnGround())
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _isFirstJump = true;
        }
        else
        {
            // 当在空中时，如果是第一次跳跃，执行二段跳
            if (_isFirstJump && _currentPlayerForm.IsJumpTwiceValid())
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            }
            _isFirstJump = false;//无论是否执行二段跳，都将第一次跳跃标记设为false
        }

    }
}
