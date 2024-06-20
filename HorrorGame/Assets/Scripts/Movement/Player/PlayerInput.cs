using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Controls _input;
    private IControllable _controllable;
    private Inventory _inventory;

    private void Awake()
    {
        _input = new Controls();
        _controllable = GetComponent<IControllable>();
        _inventory = GetComponentInChildren<Inventory>();

        _input.Player.Crouch.performed += Crouch_performed;
        _input.Player.Interacte.performed += Interacte_performed;
        _input.Player.EquipItem.performed += EquipItem_performed;
        _input.Player.DropItem.performed += DropItem_performed;
        _input.Player.ThrowItem.performed += ThrowItem_performed;
        _input.Player.UseItem.performed += UseItem_performed;

    }

    private void UseItem_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _inventory.UseItem();
    }

    private void ThrowItem_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _inventory.ThrowItem();
    }

    private void DropItem_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _inventory.DropItem();
    }

    private void EquipItem_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _inventory.TakeItem();
    }

    private void Interacte_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _controllable.Interact();
    }

    private void Crouch_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _controllable.Crouch();
    }

    private void Update()
    {
        ReadMovement();

        if (_input.Player.Run.IsPressed())//временное решение
        {
            _controllable.Run();
        }
    }

    private void ReadMovement()
    {
        var _movement = _input.Player.Move.ReadValue<Vector2>();
        Vector3 _moveDirection = (_movement.y * transform.forward + _movement.x * transform.right);

        _controllable.Move(_moveDirection);
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
