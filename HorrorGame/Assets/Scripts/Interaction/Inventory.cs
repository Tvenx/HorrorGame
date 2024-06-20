using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _currentItem;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _interactDistance;

    [SerializeField] private TMP_Text hint;

    private GameObject _interactableObject;

    private int _layerNumber = 8;
    private int _layerMask;

    private void Awake()
    {
        _playerCamera = Camera.main;
        _layerMask = 1 << _layerNumber;
    }

    private void Update()
    {
        FindItem();
    }

    public void UseItem()
    {
        _currentItem.GetComponent<Iitem>().Use();
       // Debug.Log(_currentItem);
    }

    public void TakeItem()
    {
        if (_interactableObject != null && _currentItem == null)
        {
            _currentItem = _interactableObject;
            _currentItem.GetComponent<Iitem>().Equip(transform);
            Debug.Log("взял");
        }
       
    }

    private void FindItem()
    {
        Ray ray = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _interactDistance, _layerMask))
        {
            Debug.Log(hit.collider.transform.name);
            _interactableObject = hit.collider.gameObject;
    

            hint.gameObject.SetActive(true);

            hint.text = _interactableObject.GetComponent<Iinteractable>().GetInteractionHint();
        }
        else
        {
            _interactableObject = null;
            hint.gameObject.SetActive(false);
        }
    }

    public void DropItem()
    {
        _currentItem.GetComponent<Iitem>().Drop();
        _currentItem = null;
        Debug.Log(_currentItem);
    }

    public void ThrowItem()
    {
        _currentItem.GetComponent<Iitem>().Trow();
        _currentItem = null;
    }
}
