using Core;
using UnityEngine;

namespace Character
{
    public class HumanCharacter : BaseCharacter
    {
        // TODO @Leguna: Remove Singleton, use Dependency Injection instead
        protected override void OnTap()
        {
            AudioController.Instance.PlayPlayerGetHitSound();
            GameManager.Instance.Hit(999);
        }

    }
}