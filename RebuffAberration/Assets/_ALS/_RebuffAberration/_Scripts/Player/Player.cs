using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ALS.Aberration
{
    /// <summary>
    /// Player
    /// </summary>
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private AttackRadius _attackRadius;
        [SerializeField]
        private Animator _animator;
        private Coroutine _lookCoroutine;

        [SerializeField]
        private int _health = 300;

        private const string ATTACK_TRIGGER = "Attack";

        private void Awake()
        {
            _attackRadius.OnAttack += OnAttack;
        }

        private void OnAttack(IDamageable Target)
        {
            if(_animator) _animator.SetTrigger(ATTACK_TRIGGER);

            if (_lookCoroutine != null)
            {
                StopCoroutine(_lookCoroutine);
            }

            _lookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
        }

        private IEnumerator LookAt(Transform Target)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);
            float time = 0;

            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

                time += Time.deltaTime * 2;
                yield return null;
            }

            transform.rotation = lookRotation;
        }

        public void TakeDamage(int Damage)
        {
            _health -= Damage;

            if (_health <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
