
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EquipItem : MonoBehaviour
{
    [SerializeField] private GameObject _tool;
    [SerializeField] private Transform _toolParent;

    // Start is called before the first frame update
    void Start()
    {
        _tool.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Drop();
        }
    }

    private void Drop()
    {
        _toolParent.DetachChildren();
        _tool.transform.eulerAngles = new Vector3(_tool.transform.position.x, _tool.transform.position.z, _tool.transform.position.y);
        _tool.GetComponent<Rigidbody>().isKinematic = false;
        _tool.GetComponent<MeshCollider>().enabled = true;
    }

    private void Equip()
    {
        _tool.GetComponent<Rigidbody>().isKinematic = true;
        _tool.transform.position = _toolParent.transform.position;
        _tool.transform.rotation = _toolParent.transform.rotation;

        _tool.GetComponent<MeshCollider>().enabled= false;

        _tool.transform.SetParent(_toolParent);
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
