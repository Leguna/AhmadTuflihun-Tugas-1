using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.TryGetComponent(out ZombieController controller);
        controller.Finish();
    }
}