using Misc;
using UnityEngine;
namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("Animations Settings")]
        [SerializeField] private Animator _animator;

        public void SetMoveAnimation(float speed)
        {
            _animator.SetFloat(Consts.ANIMATIONS_MOVE_SPEED, speed);
        }
        
        public void PlayAttackAnimation(bool isAttacking)
        {
            _animator.SetBool(Consts.ANIMATIONS_ATTACK, isAttacking);
            Invoke(nameof(StopAttackAnimation), 0.5f);
        }
        
        private void StopAttackAnimation()
        {
            _animator.SetBool(Consts.ANIMATIONS_ATTACK, false);
        }
    }
}