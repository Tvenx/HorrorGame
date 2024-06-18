using UnityEngine;

public class Key : MonoBehaviour, Iitem
{
    [SerializeField] private string _id;

    public string ID
    {
        get { return _id; }
        set {}
    }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Equip(Transform _toolParent)
    {
        throw new System.NotImplementedException();
    }

    public void Trow()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        Debug.Log("ключ же ничего не делает");
    }
}
