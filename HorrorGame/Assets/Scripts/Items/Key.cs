using UnityEngine;

public class Key : MonoBehaviour, Iitem
{
    [SerializeField] private string _id;

    public string ID
    {
        get { return _id; }
        set {}
    }

    public void Use()
    {
        Debug.Log("ключ же ничего не делает");
    }
}
