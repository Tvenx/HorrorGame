using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
   
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth = 1;

    [SerializeField] private int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, _minHealth - 1, _maxHealth); }
    }

    public int GetCurrentHealth()
    {
        Debug.Log($"������� ��������: {_currentHealth}");
        return CurrentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public void Kill()
    {
        Debug.Log("�� ���� �����!");
        CurrentHealth = _minHealth - 1;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if(_currentHealth < _minHealth)
        {
            Kill();
        }

        Debug.Log($"������ ����: {damage}");
    }

    public void HealthUp(int _healthPoints)
    {
        CurrentHealth += _healthPoints;
        Debug.Log($"������� �������� ������� ��: {_healthPoints}");
    }

    public void SetMaxHealth()
    {
        CurrentHealth = _maxHealth;
        Debug.Log("����������� ������������ ��������");
    }

    public void SetMinHealth()
    {
        CurrentHealth = _minHealth;
        Debug.Log("����������� ����������� ��������");
    }

}
