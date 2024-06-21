using UnityEngine;

public interface Iitem
{
    public string ID { get; set; }
    public void Use();
    public void Equip(Transform _toolParent);
    public void Drop();
    public void Trow();
}
