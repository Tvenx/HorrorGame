using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaPlayer : MonoBehaviour
{
    [SerializeField] private Slider _sliderForPower;

    private float _max = 50f;
    private float _power;
    public bool _lowPower;
    private bool _wait;

    private void Start()
    {
        _power = _max;
        _wait = true;
        _lowPower = true;
    }

    public void ExpencePower()  //расход энергии
    {
        if(_power > 0f && _lowPower)
        {
            _power -= 25.5f * Time.deltaTime;
            _sliderForPower.value = _power;
        }
        else
        {
            _lowPower = false;
            _wait = false;
        }
    }
    public void UpPower()
    {
        if(_power < _max && _wait)
        {
            _power += 5.5f * Time.deltaTime;
            _sliderForPower.value = _power;
        }
        if (_power <= 5f)
        {
            _power += 2.5f * Time.deltaTime;
            _sliderForPower.value = _power;
            if(_power >= 5f)
            {
                _lowPower = true;
                _wait = true;
            }
        }
    }
}
