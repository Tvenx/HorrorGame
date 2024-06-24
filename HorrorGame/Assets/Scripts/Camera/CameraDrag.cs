using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [SerializeField] private float _bobSpeed = 1.0f; // �������� ������
    [SerializeField] private float _bobAmount = 0.05f; // ��������� ������
    [SerializeField] private float _timer = 0.0f; // ������ ��� ������

    private Vector3 _originalCameraPosition; // �������� ������� ������

    private IControllable controllable;


    private void Start()
    {
        controllable = GetComponentInParent<IControllable>();
        print(transform.position);
        _originalCameraPosition = transform.localPosition;
        
    }

    private void Update()
    {
        if (controllable.IsMove()) // �������� �� ��������
        {
            ApplyHeadBob();
        }
        else
        {
            CancelHeadBob();
        }
    }

    private void ApplyHeadBob()
    {
            _timer += Time.deltaTime * _bobSpeed;
            float bobX = Mathf.Sin(_timer) * _bobAmount;
            float bobY = Mathf.Cos(_timer * 2) * _bobAmount * 0.5f;
            Vector3 bobOffset = new Vector3(bobX, bobY, 0);
            transform.localPosition = _originalCameraPosition + bobOffset; 
    }

    private void CancelHeadBob()
    {
        _timer = 0.0f;
        transform.localPosition = _originalCameraPosition; // ���������� ������ � �������� ���������
    }
}
