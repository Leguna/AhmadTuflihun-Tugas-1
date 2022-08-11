using Core;
using UnityEngine;

namespace Character
{
    public class ZombieCharacter : BaseCharacter
    {
        [SerializeField] private Vector2 limitXMovePos = new(2, 2);
        [SerializeField] private float moveHorizontalSpeed = 1;
        [SerializeField] private float moveHorizontalRepeatRate = 1;

        protected override void OnTap()
        {
            AudioController.Instance.PlayZombieDeadSound();
            GameManager.Instance.AddScore();
        }

        public override void Move(Vector3 dir)
        {
            base.Move(dir);
            InvokeRepeating(nameof(ZigZag), Random.Range(0, 0.3f), moveHorizontalRepeatRate);
        }

        private void ZigZag()
        {
            float direction;
            if (transform.position.x > limitXMovePos.x && transform.position.x < limitXMovePos.y)
                direction = GetRandomXDir;
            else if (transform.position.x < limitXMovePos.x)
                direction = 1;
            else
                direction = -1;

            _charRigidbody.velocity +=
                new Vector2(direction * moveHorizontalSpeed, 0);
        }

        private int GetRandomXDir => Random.Range(0, 2) * 2 - 1;

        public override void Update()
        {
            base.Update();
            var xPos = transform.position.x;
            if (xPos < limitXMovePos.x || xPos > limitXMovePos.y)
            {
                _charRigidbody.velocity = new Vector2(0, _charRigidbody.velocity.y);
            }
        }

        protected override void ReachDestination()
        {
            // TODO @Leguna: Remove Singleton, use Dependency Injection instead

            AudioController.Instance.PlayPlayerGetHitSound();
            GameManager.Instance.Hit(1);

            base.ReachDestination();
        }
    }
}