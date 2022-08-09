using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _zombieRb;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private TapableType _type = TapableType.None;

    public bool isMoving = true;

    private void OnMouseDown()
    {
        if (_type == TapableType.Human)
        {
            AudioController.Instance.PlayPlayerGetHitSound();
            GameManager.Instance.Hit(999);
        }
        else GameManager.Instance.AddScore();

        AudioController.Instance.PlayZombieDeadSound();
        GameManager.Instance.zombieControllers.Remove(this);
        Destroy(gameObject);
    }

    private void Start()
    {
        TryGetComponent(out _zombieRb);
        StartMove();
    }

    public void ToggleMove()
    {
        if (isMoving)
            StopMove();
        else
            StartMove();

        isMoving = !isMoving;
    }

    private void StopMove()
    {
        if (_zombieRb == null) return;
        isMoving = false;
        _zombieRb.velocity = Vector2.zero;
    }

    private void StartMove()
    {
        if (_zombieRb == null) return;

        isMoving = true;
        _zombieRb.velocity += Vector2.down * _moveSpeed;
    }

    public void Finish()
    {
        if (_type == TapableType.Zombie)
        {
            AudioController.Instance.PlayPlayerGetHitSound();
            GameManager.Instance.Hit(1);
        }

        GameManager.Instance.zombieControllers.Remove(this);
        Destroy(gameObject);
    }
}

enum TapableType
{
    None,
    Zombie,
    Human
}