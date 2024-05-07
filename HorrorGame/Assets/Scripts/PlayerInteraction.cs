
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Controls _inputControls;

    private void Awake()
    {
        _inputControls = new Controls();
    }
    private void Update()
    {
        Interact();
    }

    private void OnEnable()
    {
        _inputControls.Enable();
    }
    private void OnDisable()
    {
        _inputControls.Disable();
    }

    private void Interact()
    {
        if (_inputControls.Player.Interacte.triggered)
        {
            Debug.Log("Interact!");
            transform.GetComponent<ObjectPicker>().PickUp();
        }
    }
}
