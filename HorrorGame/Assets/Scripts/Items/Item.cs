using UnityEngine;

public class Item : MonoBehaviour, Iitem, Iinteractable
{
    [SerializeField] private string _id;
    [SerializeField] private float _throwForce;

    private Rigidbody _rigidbody;

    private bool _isItemInHand = false;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.isKinematic = true;
    }

    public string ID
    {
        get { return _id; }
        set { }
    }

    public void Equip(Transform _toolParent)
    {
        _rigidbody.isKinematic = true;

        transform.position = _toolParent.transform.position;
        transform.rotation = _toolParent.transform.rotation;

        transform.SetParent(_toolParent);
        _isItemInHand = true;
    }

    public void Use()
    {
        Debug.Log("ключ же ничего не делает");
    }

    public void Drop()
    {
        transform.SetParent(null);

        _rigidbody.isKinematic = false;

        _isItemInHand = false;
    }

    public void Trow()
    {
        transform.SetParent(null);

        _rigidbody.isKinematic = false;

        _rigidbody.AddForce(transform.forward * _throwForce, ForceMode.Impulse);
        _isItemInHand = false;
    }

    public string GetInteractionHint()
    {
        return ("Нажмите E чтобы экипировать");
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void InteractWith(Iitem item)
    {
        throw new System.NotImplementedException();
    }
}
