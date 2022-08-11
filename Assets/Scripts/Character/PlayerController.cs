using UnityEngine;
using Utilities;

namespace Character
{
    public class PlayerController : MonoBehaviour, IDamageable, IDoingDamage
    {
        [SerializeField] private int _health;
        public float rayRange = 4;

        public delegate void OnDamageDelegate(int damage);

        public delegate void OnDestroyDelegate();

        public event OnDamageDelegate OnDamagedEvent;
        public event OnDestroyDelegate OnDestroyedEvent;


        private void Update()
        {
            // TODO @Leguna: Ask mentor about performance, is better using Physics2DRaycaster?
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && Input.GetMouseButtonDown(0))
            {
                var interactedObject = hit.collider.gameObject;
                interactedObject.GetComponent<IInteractable>().Interact();
            }
        }

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