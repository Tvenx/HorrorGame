using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class _InteractObject : MonoBehaviour
{
    public string interactableText = "Press E to Interact";
    public UnityEvent onInteract;

    public string GetInteractionText() { return interactableText; }

    public void Interact() { onInteract.Invoke(); }
}
