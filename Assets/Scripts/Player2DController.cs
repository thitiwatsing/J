using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public float speed = 5.0f; // ความเร็วในการเดิน
    public float jumpForce = 450f; // แรงกระโดด

    private Rigidbody2D _rb; // ฟิสิกส์
    private float _moveInput; // กำหนดทิศทางการเดิน A = (-1), D = 1 / ซ้าย, ขวา
    private bool _isGrounded; // _isGrounded เช็คว่าอยู่ที่พื้นมั้ย??
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = _rb.GetComponent<SpriteRenderer>();
    }
   
    void Update()
    {
        if (Keyboard.current != null)
        {
            _moveInput = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1:0); // กด A, D ค้าง
        }
        _rb.linearVelocity = new Vector2(_moveInput * speed, _rb.linearVelocity.y); // ทิศทาง X * ความเร็ว // Y คงความเร็วเดิมไว้

        if (_moveInput < 0) { _spriteRenderer.flipX = true; }
        else if (_moveInput > 0) { _spriteRenderer.flipX = false;}

        if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded) // กด Space ทีเดียว และ ต้องอยู่ที่พื้น
        {
            _rb.AddForce(new Vector2(_rb.linearVelocity.x, jumpForce)); // คงแรงแกน X ไว้ เพิ่มแรงโดดแกน Y
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
