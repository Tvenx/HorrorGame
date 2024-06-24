using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _minEnergy = 0f;
    [SerializeField] private float _energyRestoreStep = 5f;
   
    private bool _isSpendingEnergy;

    private float _energy;
    private float _Energy
    {
        get { return _energy; }
        set { _energy = Mathf.Clamp(value, _minEnergy, _maxEnergy); }
    }

    private void Update()
    {
        if (!_isSpendingEnergy)
        {
            RestoreEnergy();
        }
    }
    private void SpendEnergy(float _energyToSpend)
    {
        if (_Energy > _minEnergy)
        {
            _Energy -= _energyToSpend * Time.deltaTime;
        }
    }

    private void RestoreEnergy()
    {
            _Energy += _energyRestoreStep * Time.deltaTime;
    }

    private float GetEnergy()
    {
        return _Energy;
    }
}
