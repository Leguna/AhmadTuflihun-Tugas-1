using Core;
using UnityEngine;
using Utilities;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseCharacter : MonoBehaviour, IInteractable, IDamageable, IDoingDamage, IMovable
    {
        public GameManager GameManager;

        [SerializeField] protected Rigidbody2D _charRigidbody;
        [SerializeField] private float _moveSpeed = 1f;

        [SerializeField] private float _destroyYPos = -4f;

        // TODO @Leguna: Uncomment this after implement hit and doing damage
        // [SerializeField] private float _damage = 1f;
        [SerializeField] private int _health = 1;

        public delegate void OnDamageDelegate(int damage);

        public delegate void OnDestroyDelegate();

        public event OnDamageDelegate OnDamagedEvent;
        public event OnDestroyDelegate OnDestroyedEvent;

        public delegate void OnReachedDestination();

        public event OnReachedDestination OnReachedDestinationEvent;
        protected abstract void OnTap();

        public void OnDestroyed()
        {
            OnDestroyedEvent?.Invoke();
        }

        public void OnDamaged(int damage)
        {
            OnDamagedEvent?.Invoke(damage);
            _health -= damage;

            if (!(_health <= 0)) return;
            OnDestroyedEvent?.Invoke();
            Destroy(gameObject);
        }

        protected virtual void ReachDestination()
        {
            OnReachedDestinationEvent?.Invoke();

            GameManager.Instance.characterControllers.Remove(this);
            Destroy(gameObject);
        }

        private void Start() => StartMove();

        public virtual void Update()
        {
            if (transform.position.y < _destroyYPos)
                ReachDestination();
        }

        public virtual void Move(Vector3 dir) => _charRigidbody.velocity = dir;

        public void StopMove() => Move(Vector3.zero);

        public void StartMove() => Move(Vector2.down * _moveSpeed);

        public void Hit(float damage)
        {
            // TODO @Leguna: Implement Hit on Player Controller
        }

        public void Interact()
        {
            // TODO @Leguna: Remove singleton and changes to dependency injection if project getting bigger
            if (GameManager.Instance.isGameOver) return;

            OnDamagedEvent?.Invoke(5);
            OnDamaged(1);

            OnTap();

            GameManager.Instance.characterControllers.Remove(this);
            OnDestroyedEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}