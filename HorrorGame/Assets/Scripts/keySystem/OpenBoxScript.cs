
using UnityEngine;

public class OpenBoxScript : MonoBehaviour
{
    readonly private string _openTrigger = "open";

    public Animator ObjectAnimator;

    [SerializeField] private GameObject _keyObjectNeeded;
    //[SerializeField] private GameObject _keyMissingText;
    //public AudioSource openSound;


    public void OpportunityToOpen()
    {
        if (_keyObjectNeeded.activeInHierarchy == true && Input.GetKey(KeyCode.E))
        {
            _keyObjectNeeded.SetActive(false);
            //openSound.Play();
            ObjectAnimator.SetBool("open",true);
            //_keyMissingText.SetActive(false);
        
        }

        else if (_keyObjectNeeded.activeInHierarchy == false && Input.GetKey(KeyCode.E))
        {
            //_keyMissingText.SetActive(true);
            Debug.Log("Locked");
        }
    }
}
