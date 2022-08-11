using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using static Utilities.IDamageable;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseCharacter : MonoBehaviour, IPointerDownHandler, IDamageable, IDoingDamage, IMovable
    {
        public GameManager GameManager;

        [SerializeField] protected Rigidbody2D _charRigidbody;
        [SerializeField] private float _moveSpeed = 1f;

        [SerializeField] private float _destroyYPos = -4f;

        // TODO @Leguna: Uncomment this after implement hit and doing damage
        // [SerializeField] private float _damage = 1f;
        [SerializeField] private float _health = 1f;

        public event OnDamaged OnDamagedEvent;
        public event OnDestroyed OnDestroyedEvent;

        public delegate void OnReachedDestination();

        public event OnReachedDestination OnReachedDestinationEvent;
        protected abstract void OnTap();

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDamagedEvent?.Invoke();
            TakeDamage(1);

            OnTap();
            GameManager.Instance.characterControllers.Remove(this);
            Destroy(gameObject);
        }

        public void TakeDamage(int damage)
        {
            OnDamagedEvent?.Invoke();
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
    }
}