using TMPro;
using UnityEngine;

public class _PlayerInteractSystem: MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _interactDistance;

    private _InteractObject _currentObject;

    [SerializeField] private TMP_Text interactText;

    private Controls _inputControls;

    private void Awake()
    {
        _inputControls = new Controls();
    }

    private void OnEnable()
    {
        _inputControls.Enable();
    }
    private void OnDisable()
    {
        _inputControls.Disable();
    }
    void Update()
    {
        RayCastInteract();
    }

    public void RayCastInteract()
    {
        Ray ray = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            _InteractObject interactableObject = hit.collider.GetComponent<_InteractObject>();
            if(interactableObject != null && interactableObject != _currentObject)
            {
                _currentObject = interactableObject;
                interactText.gameObject.SetActive(true);
              
                interactText.text = _currentObject.GetInteractionText();
               
            }
        }
        else
        {
            _currentObject = null;
            interactText.gameObject.SetActive(false);
        }

        if (_inputControls.Player.Interacte.triggered)
        {
            _currentObject?.Interact();
            
        }

    }
}
