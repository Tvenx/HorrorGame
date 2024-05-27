using TMPro;
using UnityEngine;

public class _PlayerInteractSystem: MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _interactDistance;

    [SerializeField] private TMP_Text interactText;

    private Iinteractable _currentObject;

    private Controls _inputControls;
    
    private void Awake()
    {
        _inputControls = new Controls();
        _playerCamera = Camera.main;
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

    private void RayCastInteract()
    {
        Ray ray = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            Iinteractable interactableObject = hit.collider.GetComponent<Iinteractable>();

            if(interactableObject != null && interactableObject != _currentObject)
            {
                _currentObject = interactableObject;
                interactText.gameObject.SetActive(true);
              
                interactText.text = _currentObject.GetInteractionHint();

                

            }
           
        }
        else
        {
            _currentObject = null;
            interactText.gameObject.SetActive(false);
        }

        if (_inputControls.Player.Interacte.triggered)
        {
            Iitem _itemInHand = transform.GetComponentInChildren<Iitem>();

            if (_itemInHand == null)
            {
                _currentObject?.Interact();
            }
            else
            {
                _currentObject?.InteractWith(_itemInHand);
                print(_itemInHand);
            }
        }

    }
}
