using System;
using System.Collections.Generic;
using TestGame.Scripts.Projectiles;
using UnityEngine;

namespace TestGame.Scripts.Characters.Enemy
{
    public class HumanoidEnemy : MonoBehaviour, ITakeDamage
    {
        [SerializeField]
        private HumanoidEnemyController _controller;
        [SerializeField]
        private EnemyAnimationController _animationController;
        [SerializeField]
        private Transform _targetPoint;
        [SerializeField]
        private List<TakeDamageType> _typesCanBeDamaged;
        public GameObject Object => gameObject;

        public Vector3 TargetPosition => _targetPoint.position;

        public TakeDamageType TakeDamageType => TakeDamageType.Enemy;

        public event Action<HumanoidEnemy> OnDeathEvent;
        public event Action OnTakeDamageEvent;

        public void Initialize()
        {
            if (_controller == null)
                throw new Exception("Add HumanoidEnemyController component in inspector!");
            if (_animationController == null)
                throw new Exception("Add EnemyAnimationController component in inspector!");
            if (_targetPoint == null)
                throw new Exception("Add Transform component in inspector!");

            _controller.Initialize();
            _controller.OnStartMovingEvent += _animationController.MovingAnimation;
            _controller.OnSetTargetEvent += _animationController.TriggeredAnimation;
            _animationController.OnAngryCompleteEvent += _controller.StartMoving;
        }

        public void TakeDamage()
        {
            _animationController.OnTakeDamage(TargetPosition);
            OnDeathEvent?.Invoke(this);
            Destroy(gameObject);
        }
        public void OnWin()
        {
            _controller.CanMove = false;
            _animationController.„RelebrateAnimation();
        }
        private void DoingDamage(ITakeDamage target)
        {
            target.TakeDamage();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<ITakeDamage>();

            if (target != null && _typesCanBeDamaged.Contains(target.TakeDamageType))
            {
                DoingDamage(target);
            }
        }
    }
}

