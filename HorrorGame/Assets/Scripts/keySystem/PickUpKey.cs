
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] private GameObject _keyObject;
    [SerializeField] private GameObject _objectInInvent;
    //public AudioSource keySound;

    void Start()
    {
        _objectInInvent.SetActive(false);
    }

    public void PickUpKeyInInventory()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _keyObject.SetActive(false);
            //keySound.Play();
            _objectInInvent.SetActive(true);
        }
    }
}
