using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    private float _maxCharge = 100f;
    [SerializeField] private float _currentCharge;
    private bool on;
    private bool off;
    private Coroutine chargeCoroutine; // ƒобавл€ем переменную дл€ хранени€ ссылки на корутину

    private void Start()
    {
        _currentCharge = _maxCharge;
        flashlight.GetComponent<Light>().enabled = false;
        off = true;
    }

    private void Update()
    {
        Use();
    }

    private void Use()
    {
        if (off && Input.GetKeyDown(KeyCode.Y))
        {
            flashlight.GetComponent<Light>().enabled = true;
            off = false;
            on = true;
            chargeCoroutine = StartCoroutine(DecreaseChargeOverTime()); // «апускаем корутину и сохран€ем ссылку
        }
        else if (on && Input.GetKeyDown(KeyCode.Y))
        {
            flashlight.GetComponent<Light>().enabled = false;
            off = true;
            on = false;
            if (chargeCoroutine != null) // ќстанавливаем корутину, если она была запущена
            {
                StopCoroutine(chargeCoroutine);
            }
        }
    }

    private System.Collections.IEnumerator DecreaseChargeOverTime()
    {
        while (_currentCharge > 0)
        {
            // ”меньшаем зар€д каждые несколько секунд
            yield return new WaitForSeconds(2f);
            _currentCharge -= 1f; // »змените шаг уменьшени€ зар€да по своему усмотрению
            Debug.Log("Current charge: " + _currentCharge);
        }
    }
}
