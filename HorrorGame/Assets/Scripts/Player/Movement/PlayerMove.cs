using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Controls _input;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private CharacterController _characterController;
    private Vector2 _movement;
   
    float velocity;

    private void Awake()
    {
        _input = new Controls();
        _characterController = transform.gameObject.GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        Walk();
        GravityFall();
        Jump();
    }

    private void Walk()
    {
        _movement = _input.Player.Move.ReadValue<Vector2>();
        Vector3 _moveDirection = (_movement.y * transform.forward + _movement.x * transform.right);
        _characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void GravityFall()
    {
        _characterController.Move(Vector3.up * velocity * Time.deltaTime);
        velocity += -10 * Time.deltaTime;
    }

    private void Jump()
    {
        if (_input.Player.Jump.triggered && _characterController.isGrounded)
        {
            velocity = _jumpForce;
        }
    }

}
