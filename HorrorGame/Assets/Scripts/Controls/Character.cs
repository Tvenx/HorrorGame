using UnityEngine;

public class Character : MonoBehaviour, IControllable
{
    private float _speed;
    [SerializeField] private float _walkSpeed;

    private CharacterController _characterController;
    private InteractSystem _interactSystem;

    private float velocity;

    private bool _isMove; 

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _interactSystem = GetComponentInChildren<InteractSystem>();

        _speed = _walkSpeed;
    }

    private void Update()
    {
        GravityFall();
    }

    private void GravityFall()
    {
        if (!_characterController.isGrounded)
        {
            _characterController.Move(Vector3.up * velocity * Time.deltaTime);
            velocity += -10 * Time.deltaTime;
        }
    }

    public void Crouch()
    {
        Debug.Log("Крадёмся");
    }

    public void Interact()
    {
       _interactSystem.Interact();
    }

    public void Look()
    {
        throw new System.NotImplementedException();
    }

    public void Run()
    {
        Debug.Log("Бежит");
    }

    public void Move(Vector3 _direction)
    {
        if (_direction != Vector3.zero)
        {
            _isMove = true;
            _characterController.Move(_direction * _speed * Time.deltaTime);
        }
        else
        {
            _isMove = false;
        }
    }

    public bool IsMove()
    {
        return _isMove;
    }
}
