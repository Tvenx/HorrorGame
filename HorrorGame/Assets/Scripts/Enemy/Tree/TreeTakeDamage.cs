using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTakeDamage : MonoBehaviour
{
    [SerializeField] private int damaged = 10;

    private TreeHealth _treeHealth;

    public void Active()
    {
        _treeHealth.TakeDamage(damaged);
        gameObject.SetActive(false);
    }
}
