using NUnit.Framework;
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    private Controls _input;
    private Animator _animatorPlayer;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _crouchSpeed;

    private CharacterController _characterController;
    private CapsuleCollider _capsuleCollider;

    private Vector2 _movement;

    private bool _isCrouching = false;
    private float _originalMaxHeight;
    private float _minHeight = 1.0f;
    

    float velocity;

    private void Awake()
    {
        _input = new Controls();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _characterController = transform.gameObject.GetComponent<CharacterController>();
        _originalMaxHeight = _characterController.height;
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
        Crouch();
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


    private void Crouch()   //приседание
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!_isCrouching)
            {
                StartCoroutine(CrouchCoroutine(_minHeight, _crouchSpeed)); // Рост в минимальное 
               
            }
            else
            {
                StartCoroutine(StandUpCoroutine(_originalMaxHeight, _crouchSpeed)); //Возращает рост игрока
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
        print("Присед");
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
        print("Вставание");
    }
}
