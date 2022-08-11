using UnityEngine;
using Utilities;

namespace Character
{
    public class PlayerController : MonoBehaviour, IDamageable, IDoingDamage
    {
        [SerializeField] private int _health;

        public delegate void OnDamageDelegate(int damage);

        public delegate void OnDestroyDelegate();

        public event OnDamageDelegate OnDamagedEvent;
        public event OnDestroyDelegate OnDestroyedEvent;


        public void OnDestroyed()
        {
            OnDestroyedEvent?.Invoke();
        }

        public void OnDamaged(int damage)
        {
            OnDamagedEvent?.Invoke(damage);
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