namespace Utilities
{
    public interface IDamageable
    {
        public delegate void OnDamaged();

        public event OnDamaged OnDamagedEvent;

        public delegate void OnDestroyed();

        public event OnDestroyed OnDestroyedEvent;
        void TakeDamage(int damage);
    }
}