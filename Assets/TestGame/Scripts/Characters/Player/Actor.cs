using Assets.TestGame.Scripts.Player;
using SpaceTraveler.GameStructures.Zones;
using System;
using TestGame.Scripts.Projectiles;
using UnityEngine;

namespace TestGame.Scripts.Characters.Player
{
    [RequireComponent(typeof(ActorController), typeof(ActorAniamtionController))]
    public class Actor : MonoBehaviour, ITakeDamage, ITriggerObject
    {
        [SerializeField]
        private ActorController _controller;
        [SerializeField]
        private ActorAniamtionController _animationController;
        [SerializeField]
        private GameObject _flashLight;

        public Vector3 TargetPosition => transform.position;

        public Vector3 Position => transform.position;
        public TriggerObjectType TriggerType => TriggerObjectType.Player;

        public TakeDamageType TakeDamageType => TakeDamageType.Player;

        public event Action LooseEvent;
        public event Action OnTakeDamageEvent;
        public void Initialize()
        {

            if (_controller == null)
                throw new Exception("Add ActorController component in inspector!");
            if (_animationController == null)
                throw new Exception("Add ActorAniamtionController component in inspector!");

            _controller.Initialize();
            _animationController.Initialize();

            _controller.OnStartMovingEvent += _animationController.MovingAnimation;
            _controller.OnСhooseTargetEvent += _animationController.TryShootAnimation;
            _animationController.OnShootEvent += _controller.Shoot;
            _animationController.OnEndShootAnimEvent += _controller.StartMoving;
            _animationController.OnLooseAnimEndEvent += OnLoose;
        }
        public void FlashlightActivity(TimeOfDay time)
        {
            switch (time)
            {
                case TimeOfDay.Night:
                    {
                        _flashLight.SetActive(true);
                        break;
                    }
                case TimeOfDay.Day:
                    {
                        _flashLight.SetActive(false);
                        break;
                    }
            }
        }
        public void StartMoving()
        {
            _controller.Enable();
            _controller.StartMoving();
        }
        public void TakeDamage()
        {
            _controller.OnTakeDamage();
            _animationController.DefeatAnimation();
        }
        public void OnWin()
        {
            _controller.Disable();
            _animationController.IdleAnimation();

        }
        private void OnLoose()
        {
            LooseEvent?.Invoke();
        }
    }
}
