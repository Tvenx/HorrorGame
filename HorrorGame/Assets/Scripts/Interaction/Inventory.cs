using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject _currentItem;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _interactDistance;

    [SerializeField] private TMP_Text interactText;

    private GameObject _interactableObject;

    private void Awake()
    {
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        FindItem();
    }

    private void UseItem()
    {
        _currentItem.GetComponent<Iitem>().Use();
    }

    public void TakeItem()
    {
        if (_currentItem != null)
        {
            _currentItem.GetComponent<Iitem>().Equip(transform);
        }
       
    }

    private void FindItem()
    {
        Ray ray = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            _interactableObject = hit.collider.gameObject;

            if (_interactableObject.GetComponent<Iitem>() != null && _interactableObject != _currentItem)
            {
                _currentItem = _interactableObject;
                interactText.gameObject.SetActive(true);

                interactText.text = _currentItem.GetComponent<Iinteractable>().GetInteractionHint();
            }
        }
        else
        {
            _interactableObject = null;
            interactText.gameObject.SetActive(false);
        }
    }

    public void DropItem()
    {
        _currentItem.GetComponent<Iitem>().Drop();
        _currentItem = null;
    }

    public void ThrowItem()
    {
        _currentItem.GetComponent<Iitem>().Trow();
        _currentItem = null;
    }
}
