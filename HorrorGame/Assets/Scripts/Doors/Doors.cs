
using UnityEngine;

public class Doors : MonoBehaviour
{
    readonly private string _openTrigger = "open"; 
    readonly private string _closeTrigger = "close"; 

    [SerializeField] bool toggle;
    public Animator door;

    public void openClose()
    {
        toggle = !toggle;

        if(toggle == false)
        {
            door.ResetTrigger(_openTrigger);
            door.SetTrigger(_closeTrigger);
        }
        if(toggle == true)
        {
            door.ResetTrigger(_closeTrigger);
            door.SetTrigger(_openTrigger);
        }
    }
}
