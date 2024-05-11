
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EquipItem : MonoBehaviour
{
    [SerializeField] private GameObject _tool;
    [SerializeField] private Transform _toolParent;

    [SerializeField] private float _throwForce;

    private bool _isItemInHand = false;

    // Start is called before the first frame update
    void Start()
    {
        _tool.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isItemInHand == true)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Drop();
            }
            if (Input.GetKey(KeyCode.Space))
            {

                Throw(transform.forward * _throwForce);
            }
        }
    }

    private void Drop()
    {
        _toolParent.DetachChildren();
        _tool.transform.eulerAngles = new Vector3(_tool.transform.position.x, _tool.transform.position.z, _tool.transform.position.y);
        _tool.GetComponent<Rigidbody>().isKinematic = false;
        _tool.GetComponent<MeshCollider>().enabled = true;
        _isItemInHand = false;
    }

    private void Equip()
    {
        _tool.GetComponent<Rigidbody>().isKinematic = true;
        _tool.transform.position = _toolParent.transform.position;
        _tool.transform.rotation = _toolParent.transform.rotation;

        _tool.GetComponent<MeshCollider>().enabled = false;

        _tool.transform.SetParent(_toolParent);
        _isItemInHand = true;
    }

    public void Throw(Vector3 force)
    {
        transform.SetParent(null);
        _tool.GetComponent<Rigidbody>().isKinematic = false;
        _tool.GetComponent<MeshCollider>().enabled = true;

        if (force.sqrMagnitude > 0f)
        {
            _tool.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            _isItemInHand = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Equip();
            }
        }
    }
}
