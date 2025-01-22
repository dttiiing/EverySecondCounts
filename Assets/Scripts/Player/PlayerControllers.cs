using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    ///<summary>玩家键盘/控制器输入控制</summary>
    public PlayerInputControl inputControl;
    ///<summary>角色刚体</summary>
    public Rigidbody2D _rigidbody;
    ///<summary>玩家输入方向</summary>
    private Vector2 inputDirection;
    ///<summary>移动速度</summary>
    [SerializeField] private float speed;
    ///<summary>跳跃力量</summary>
    [SerializeField] private float jumpForce;
    ///<summary>物理检查组件</summary>
    private PhysicsCheck physicsCheck;
    public bool isDead;

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
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    ///<summary>固定更新，处理玩家移动逻辑</summary>
    private void FixedUpdate()
    {
        PlayerMove();
    }

    ///<summary>初始化角色属性和组件</summary>
    private void Init()
    {
        speed = 270;
        jumpForce = 16.5f;
        _rigidbody = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();
        physicsCheck = GetComponent<PhysicsCheck>();
    }
    ///<summary>控制玩家移动</summary>
    public void PlayerMove()
    {
        // 根据输入方向计算速度，保持与帧率无关的平稳移动
        _rigidbody.velocity = new Vector2(inputDirection.x * speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
        PlayerTransForm(inputDirection.x);
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
        if (physicsCheck.IsGround())
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
