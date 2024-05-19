public interface IHealth
{
    public void RecountHealth(int damage);
    public int GetCurrentHealth();
    public int GetMaxHealth();
    public void SetMaxHealth();
    public void Kill();
}