using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _baseDuration = 10f;

        private ITakeDamage target;
        private float speed;

        public Action<Projectile> OnDisableObject { get; set; }

        /// <summary>
        /// Initialize the projectile with each shot.
        /// </summary>
        /// <param name="target">The target at which the shot was fired</param>
        /// <param name="speed">Projectile velocity</param>
        public void Initialize(ITakeDamage target, float speed)
        {
            if (target != null)
            {
                this.target = target;
                this.speed = speed;

                target.OnTakeDamageEvent += OnTargetDeath;

                StartMove();
            }
            else
                gameObject.SetActive(false);

        }


        private void StartMove()
        {
            transform.DOMove(target.TargetPosition, _baseDuration / speed);
        }

        private void DoingDamage()
        {
            target.TakeDamage();
            gameObject.SetActive(false);
        }
        private void OnTargetDeath()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var obj = collision.gameObject.GetComponent<ITakeDamage>();
            if (obj == target)
            {
                DoingDamage();
            }
        }
    }
}

