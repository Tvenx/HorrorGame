using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EquipItem : MonoBehaviour
{
    [SerializeField] private GameObject _tool;
    [SerializeField] private Transform _toolParent;
    [SerializeField] private float _throwForce;

   /* private Rigidbody _rigidbody;

    private bool _isItemInHand = false;

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

    private void Start()
    {
        _rigidbody = _tool.GetComponent<Rigidbody>();

        _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (_isItemInHand == true)
        {
            if (_inputControls.Player.Drop.triggered)
                Drop();
      
            if (_inputControls.Player.Throw.triggered)
                Throw(_throwForce);
        }
    }

    private void Drop()
    {
        transform.SetParent(null);
       
        _rigidbody.isKinematic = false;

        _isItemInHand = false;
    }

    public void Equip()
    {
       _rigidbody.isKinematic = true;

        _tool.transform.position = _toolParent.transform.position;
        _tool.transform.rotation = _toolParent.transform.rotation;

        _tool.transform.SetParent(_toolParent);
        _isItemInHand = true;
    }

    public void Throw(float force)
    {
        transform.SetParent(null);


        _tool.GetComponent<Rigidbody>().isKinematic = false;
     
         _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        _isItemInHand = false;
    }*/
}
