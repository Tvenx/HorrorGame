using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    private Controls _input;

    [Header("HeadBob")]
    [SerializeField] private float _bobSpeed = 1.0f; // Скорость тряски
    [SerializeField] private float _bobAmount = 0.05f; // Амплитуда тряски
    [SerializeField] private float _timer = 0.0f; // Таймер для тряски
    private Vector3 _originalCameraPosition; // Исходная позиция камеры

    [Header("ChatacterMove")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _crouchSpeed;
    private float _speed;

    private CharacterController _characterController;

    private StaminaPlayer _staminaPlayer;

    private Vector2 _movement;

    private bool _isCrouching = false;
    private float _originalMaxHeight;
    private float _crouchHeight = 1.0f;
    float _characterHeight;

    private float velocity;


    private void Awake()
    {
        _input = new Controls();

        _characterController = transform.gameObject.GetComponent<CharacterController>();
        _originalMaxHeight = _characterController.height;
        _staminaPlayer = GetComponent<StaminaPlayer>();

        _speed = _walkSpeed;
        _characterHeight = _characterController.height;

        _originalCameraPosition = transform.GetChild(0).localPosition;
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

        ApplyHeadBob();
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
                if (!Physics.Raycast(_ray, _characterHeight - _crouchHeight + 0.4f)) //исправить это надо будет обязательно
                {
                    StartCoroutine(SetHeight(_originalMaxHeight, _crouchSpeed));
                    _isCrouching = false;
                }
            }
        }
    }
    //к этому тоже много вопросов
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

    private void ApplyHeadBob()
    {
        if (_movement.magnitude > 0.1f) // Проверка на движение
        {
            _timer += Time.deltaTime * _bobSpeed;
            float bobX = Mathf.Sin(_timer) * _bobAmount;
            float bobY = Mathf.Cos(_timer * 2) * _bobAmount * 0.5f;
            Vector3 bobOffset = new Vector3(bobX, bobY, 0);
            transform.GetChild(0).localPosition = _originalCameraPosition + bobOffset; 
        }
        else
        {
            _timer = 0.0f;
            transform.GetChild(0).localPosition = _originalCameraPosition; // Возвращаем камеру в исходное положение
        }
    }
}
