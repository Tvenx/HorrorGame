using UnityEngine;
using UnityEngine.UI;

public class StaminaPlayer : MonoBehaviour
{
    [SerializeField] private Slider _sliderForPower;

    [SerializeField] private float _maxEnergy = 50f;
    
    private const float _energyDownStep = 25.5f;
    private const float _energyUpStep = 2.5f;

    private float _energy;
    public float _Energy
    {
        get { return _energy; }
        set { _energy = Mathf.Clamp(value, 0, _maxEnergy);}
    }

    private void Start()
    {
        _Energy = _maxEnergy;
    }

    public void LowEnergy()  //расход энергии
    {
            print("понижаем энергию");
            _Energy -= _energyDownStep * Time.deltaTime;
            _sliderForPower.value = _energy;
    }

    public void UpEnergy()
    {
            _Energy += _energyUpStep * Time.deltaTime;
            _sliderForPower.value = _Energy;
    }

    public bool CanRun()
    {
        if (_energy <= 1f)
        {
            return false;
        }
        return true;
    }
}
