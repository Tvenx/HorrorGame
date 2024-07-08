using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour, Iusable
{
    public GameObject flashlight;

    public bool on;
    public bool off;

    void Start()
    {
        flashlight.gameObject.GetComponent<Light>().enabled = false;
        off = true;
    }

    private void Update()
    {
        Use();
    }

    public void Use()
    {
        if (off && Input.GetKeyDown(KeyCode.Y))
        {
            flashlight.gameObject.GetComponent<Light>().enabled = true;
            off = false;
            on = true;
        }
        else if (on && Input.GetKeyDown(KeyCode.Y))
        {
            flashlight.gameObject.GetComponent<Light>().enabled = false;
            off = true;
            on = false;
        }
    }
}
