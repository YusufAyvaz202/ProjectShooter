using Managers;
using UnityEngine;
using Misc;
namespace Player.Animations
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("Animations Settings")]
        [SerializeField] private Animator _animator;

        private void ChangeMoveAnimationParameter(float speed)
        {
            _animator.SetFloat(Consts.ANIMATIONS_MOVE_SPEED, speed);
        }

        #region Initialize & Cleanup

        private void OnEnable()
        {
            EventManager.PlayerMoveAnimationParameterChanged += ChangeMoveAnimationParameter;
        }

        private void OnDisable()
        {
            EventManager.PlayerMoveAnimationParameterChanged -= ChangeMoveAnimationParameter;
        }

        #endregion
    }
}