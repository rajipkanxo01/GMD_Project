using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int Attack1 = Animator.StringToHash("attack1");
        private static readonly int RunningAttack = Animator.StringToHash("runAttack");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

                if (stateInfo.IsName("Running") || _animator.GetBool("move"))
                {
                    _animator.SetTrigger(RunningAttack);
                    Debug.Log("Player performed a running attack!");
                }
                else
                {
                    _animator.SetTrigger(Attack1);
                    Debug.Log("Player performed a normal attack!");
                }
            }
        }
    }
}