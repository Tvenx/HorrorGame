using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Controls _input;
    [SerializeField] private float _speed;
    private CharacterController characterController;
    private Vector2 _movement;
    [SerializeField] private float _jumpForce;
    float velocity;

    private void Awake()
    {
        _input = new Controls();
        characterController = transform.gameObject.GetComponent<CharacterController>();
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
        characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void GravityFall()
    {
       

            characterController.Move(Vector3.up * velocity * Time.deltaTime);
        velocity += -10 * Time.deltaTime;
    }

    private void Jump()
    {

        if (_input.Player.Jump.triggered && characterController.isGrounded)
        {
            velocity = _jumpForce;
        }
       
    }

}
