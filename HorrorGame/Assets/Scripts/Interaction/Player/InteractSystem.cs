using TMPro;
using UnityEngine;

public class InteractSystem: MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _interactDistance;

    [SerializeField] private TMP_Text interactText;

    private Iinteractable _currentObject;
    
    private void Awake()
    {
        _playerCamera = Camera.main;
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
    }

    public void Interact()
    {
      
    }
}
