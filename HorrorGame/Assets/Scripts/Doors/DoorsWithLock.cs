
using UnityEngine;

public class DoorsWithLock : MonoBehaviour
{
    readonly private string _openTrigger = "open";
    readonly private string _closeTrigger = "close";

    private bool toggle;

    [SerializeField] private Animator _door;
    [SerializeField] private GameObject _keyInInventory;

    //public AudioSource doorSound;
    //public AudioSource lockedSound;

    private bool _unlocked;
    private bool _locked;
    private bool _hasKey;

    void Start()
    {
        _hasKey = false;
        _unlocked = false;
        _locked = true;
    }

    public void OpportunityToOpen()
    {
        if (_keyInInventory.activeInHierarchy == true && Input.GetKey(KeyCode.E))
        {
            //openSound.Play();
            //_keyMissingText.SetActive(false);
            _unlocked = true;
            if (_unlocked == true)
            {
                openClose();
            }

        }
        else if (_keyInInventory.activeInHierarchy == false && Input.GetKey(KeyCode.E))
        {
            //_keyMissingText.SetActive(true);
            Debug.Log("Locked");
        }
    }

    public void openClose()
    {
        toggle = !toggle;

        if (toggle == false)
        {
            _door.ResetTrigger(_openTrigger);
            _door.SetTrigger(_closeTrigger);
        }
        if (toggle == true)
        {
            _door.ResetTrigger(_closeTrigger);
            _door.SetTrigger(_openTrigger);
        }
    }
}
