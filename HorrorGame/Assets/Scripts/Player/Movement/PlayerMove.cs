using NUnit.Framework;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerMove : MonoBehaviour
{
    private Controls _input;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _crouchSpeed;

    private CharacterController _characterController;

    [SerializeField] private float _rayDistance;
    [SerializeField] private LayerMask _ceiling;    // слой потолок 

    private StaminaPlayer _staminaPlayer;   //Класс стамина выносливости

    private Vector2 _movement;

    private bool _running = true;
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
        Crouch();
        Run();
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


    private void Run()
    {
        if (_running)
        {
            if (Input.GetKey(KeyCode.LeftShift) && _staminaPlayer._lowPower)
            {
                _staminaPlayer.ExpencePower();
                _movement = _input.Player.Move.ReadValue<Vector2>();
                Vector3 _moveDirection = (_movement.y * transform.forward + _movement.x * transform.right);
                _characterController.Move(_moveDirection * _speed * 4 * Time.deltaTime);    //скорость увеличивается на 4
                print("Shift Зажат");
            }

            if (!Input.GetKey(KeyCode.LeftShift) || !_staminaPlayer._lowPower)
            {
                print("Shift отпущен");
                _staminaPlayer.UpPower();
            }

        }
    }


    private void Crouch()   //приседание
    {
        Ray _ray = new Ray(transform.position, Vector3.up);     // луч (если своими словами, то он для проверки, если игрок в приседе и под низким объектом,то заблокировать выход из приседа.

        if(Physics.Raycast(_ray, _rayDistance, _ceiling))
        {
            _dontUp = false;
        }
        else
        {
            _dontUp = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!_isCrouching)
            {
                StartCoroutine(CrouchCoroutine(_minHeight, _crouchSpeed)); // Рост в минимальное
                _running = false;


            }
            else if(_isCrouching && _dontUp)
            {
                StartCoroutine(StandUpCoroutine(_originalMaxHeight, _crouchSpeed)); //Возращает рост игрока
                _running = true;
            }
        }
    }





    //коруутины

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
