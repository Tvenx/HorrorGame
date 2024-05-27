using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _closedOnKey;
    [SerializeField] private bool _closed;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void InsertKey()
    {
        if(_closedOnKey)
        {
            Debug.Log("использован ключ");
            _closedOnKey = false;
            Open();
        }
        else
        {
            Close();
            _closedOnKey = true;
        }
    }

    public void SwitchState()
    {
        if (!_closedOnKey)
        {
            if (_closed)
            {
                Open();
              //  Debug.Log("jnrhsdf.");
            }
            else
            {
                Close();
            }
        }
        else
        {
            Debug.Log("закрыто на ключ");
        }
    }

    private void Open()
    {
        _animator.SetTrigger("Open");
        Debug.Log("Открыл");
        _closed = false;
    }

    private void Close()
    {
        _animator.SetTrigger("Close");
        Debug.Log("Закрыл");
        _closed = true;
    }
}
