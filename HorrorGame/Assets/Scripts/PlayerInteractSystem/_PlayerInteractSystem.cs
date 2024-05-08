using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class _PlayerInteractSystem: MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _interactDistance;

    private _InteractObject _currentObject;

    public GameObject interactText;
    
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
                interactText.SetActive(true);
                TextMeshProUGUI textComponent = interactText.GetComponent<TextMeshProUGUI>();

                if(textComponent != null)
                {
                    textComponent.text = _currentObject.GetInteractionText();
                }
            }
        }
        else
        {
            _currentObject = null;
            interactText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentObject?.Interact();
        }

    }
}
