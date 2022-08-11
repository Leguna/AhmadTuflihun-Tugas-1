namespace Utilities
{
    public interface IDamageable
    {
        void OnDestroyed();
        void OnDamaged(int damage);
    }
}