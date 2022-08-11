using UnityEngine;

namespace Character
{
    public interface IMovable
    {
        void StartMove();
        void Move(Vector3 dir);
        void StopMove();
    }
}