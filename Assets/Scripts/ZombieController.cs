using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _zombieRb;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private TapableType _type = TapableType.None;

    private void OnMouseDown()
    {
        if (_type == TapableType.Human) GameManager.Instance.Hit(3);
        else GameManager.Instance.AddScore();


        Destroy(gameObject);
    }

    private void Start()
    {
        TryGetComponent(out _zombieRb);
        StartMove();
    }

    private void StartMove()
    {
        if (_zombieRb == null) return;

        _zombieRb.velocity += Vector2.down * _moveSpeed;
    }

    public void Finish()
    {
        if (_type == TapableType.Zombie)
            GameManager.Instance.Hit(1);

        Destroy(gameObject);
    }
}

enum TapableType
{
    None,
    Zombie,
    Human
}