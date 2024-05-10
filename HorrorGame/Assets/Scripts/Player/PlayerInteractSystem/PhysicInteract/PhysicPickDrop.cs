using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicPickDrop : MonoBehaviour
{
    [SerializeField] private Transform _playerCameraTransform;
    [SerializeField] private Transform _objectGrabPointTransform;
    [SerializeField] private LayerMask _pickUpLayerMask;

    private ObjectGrabable _objectGrabable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_objectGrabable == null)
            {
                float pickUpDistance = 2f;
                if (Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, _pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out _objectGrabable))
                    {
                        _objectGrabable.Grab(_objectGrabPointTransform);
                    }
                }
            }
            else
            {
                _objectGrabable.Drop();
                _objectGrabable = null;
            }
        }
    }
}
