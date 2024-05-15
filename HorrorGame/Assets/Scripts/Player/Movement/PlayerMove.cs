using NUnit.Framework;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerMove : MonoBehaviour
{
    private Controls _input;
    bool _running;

    private float _speed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _crouchSpeed;

    private CharacterController _characterController;

   

    private StaminaPlayer _staminaPlayer;   //Класс стамина выносливости

    private Vector2 _movement;

    private bool _isCrouching = false;
    private bool _dontUp = true;
    private float _originalMaxHeight;
    private float _minHeight = 1.0f;
    

    float velocity;

    private void Awake()
    {
        _input = new Controls();

        _characterController = transform.gameObject.GetComponent<CharacterController>();
        _originalMaxHeight = _characterController.height;
        _staminaPlayer = GetComponent<StaminaPlayer>();

        _speed = _walkSpeed;
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!_isCrouching)
            {
                StartCoroutine(CrouchCoroutine(_minHeight, _crouchSpeed)); 
                _running = false;
            }
            else
            {
                Ray _ray = new Ray(transform.position, Vector3.up);
                RaycastHit hit;
                if (!Physics.Raycast(_ray, out hit, _minHeight))
                {
                    StartCoroutine(StandUpCoroutine(_originalMaxHeight, _crouchSpeed)); 
                    _running = true;
                }
            }  
        }
    }

    private IEnumerator CrouchCoroutine(float targetHeight, float duration) //присед
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
        _isCrouching = true;
        print("ЛЕЖАТЬ");
    }

    IEnumerator StandUpCoroutine(float targetHeight, float duration)    //выход из приседа
    {
        float time = 0f;
        float startHeight = _characterController.height;
        Vector3 startPosition = transform.position + Vector3.up * (_originalMaxHeight - startHeight) / 2f;

        while (time < duration)
        {
            float newHeight = Mathf.Lerp(startHeight, targetHeight, time / duration);
            Vector3 newPosition = startPosition - Vector3.up * (_originalMaxHeight - newHeight) / 2f;

            _characterController.height = newHeight;
            transform.position = newPosition;

            time += Time.deltaTime;
            yield return null;
        }

        _characterController.height = targetHeight;
        _isCrouching = false;
        print("ПОДЪЕМ");
    }
}
