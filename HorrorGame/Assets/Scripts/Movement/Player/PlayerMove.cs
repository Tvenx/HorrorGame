using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    private Controls _input;

    private float _speed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _crouchSpeed;

    private CharacterController _characterController;   

    private StaminaPlayer _staminaPlayer;  

    private Vector2 _movement;

    private bool _isCrouching = false;
    private float _originalMaxHeight;
    private float _crouchHeight = 1.0f;
    float _characterHeight;


    float velocity;

    private void Awake()
    {
        _input = new Controls();

        _characterController = transform.gameObject.GetComponent<CharacterController>();
        _originalMaxHeight = _characterController.height;
        _staminaPlayer = GetComponent<StaminaPlayer>();

        _speed = _walkSpeed;
        _characterHeight = _characterController.height;
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
        Move();
        GravityFall();
        Crouch();
        Run();
    }

    private void Move()
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


    private void Run()
    {
            if (_input.Player.Run.IsPressed() && _staminaPlayer.CanRun() && !_isCrouching)
            {
                _staminaPlayer.LowEnergy();
                _speed = _runSpeed;
            }
            else
            {
                 _speed = _walkSpeed;
                _staminaPlayer.UpEnergy();
            }
    }


    private void Crouch()   //приседание
    {
        if (_input.Player.Crouch.triggered)
        {
            if (!_isCrouching)
            {
                StartCoroutine(SetHeight(_crouchHeight, _crouchSpeed)); 
                _isCrouching = true;
            }
            else
            {
                Ray _ray = new Ray(transform.position, Vector3.up);
                if (!Physics.Raycast(_ray, _characterHeight - _crouchHeight))
                {
                    StartCoroutine(SetHeight(_originalMaxHeight, _crouchSpeed)); 
                    _isCrouching = false;
                }
            }  
        }
    }

    private IEnumerator SetHeight(float targetHeight, float duration) //присед
    {
        float time = 0f;
        float startHeight = _characterController.height;

        while (time < duration)
        {
            float newHeight = Mathf.Lerp(startHeight, targetHeight, time / duration);
            _characterController.height = newHeight;
            time += Time.deltaTime;
            yield return null;
        }
        _characterController.height = targetHeight;
    }
}
