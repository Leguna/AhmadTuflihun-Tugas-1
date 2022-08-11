using UnityEngine;
using Utilities;

namespace Character
{
    public class PlayerController : MonoBehaviour, IDamageable, IDoingDamage
    {
        [SerializeField] private int _health;
        public event IDamageable.OnDamaged OnDamagedEvent;
        public event IDamageable.OnDestroyed OnDestroyedEvent;

        public void TakeDamage(int damage)
        {
            OnDamagedEvent?.Invoke();
            _health -= damage;
            if (_health <= 0)
            {
                OnDestroyedEvent?.Invoke();
                enabled = false;
            }
        }

        public void Hit(float damage)
        {
            // TODO @Leguna: Implement hit on click BaseCharacter
        }
    }
}