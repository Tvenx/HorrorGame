using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Flashlight : MonoBehaviour, Iusable
{
    [SerializeField] private float _currentCharge;
    private Light _light;
    private float _maxCharge = 100f;
    private bool switchValue;

    private void Start()
    {
        _currentCharge = _maxCharge;

        _light = GetComponentInChildren<Light>();

        switchValue = _light.enabled;
    }

    public void Use()
    {
        if (_currentCharge > 0)
        {
            switchValue = !switchValue;

            if (switchValue == true)
            {
                SwitchOn();
            }
            else
            {
                SwitchOff();
            }
        }
    }

    private void SwitchOn()
    {
        _light.enabled = true;
        StartCoroutine(DecreaseChargeOverTime());
    }

    private void SwitchOff()
    {
        _light.enabled = false;
        StopAllCoroutines();
    }

    private IEnumerator DecreaseChargeOverTime()
    {
        while (_currentCharge > 0)
        {
            // ”меньшаем зар€д каждые несколько секунд
            yield return new WaitForSeconds(2f);
            _currentCharge -= 1f; // »змените шаг уменьшени€ зар€да по своему усмотрению
            Debug.Log("Current charge: " + _currentCharge);
        }

        if(_currentCharge  == 0)
        {
            _light.enabled = false;
        }
    }
}
