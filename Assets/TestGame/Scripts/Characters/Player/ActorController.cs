using Assets.TestGame.Scripts.Player;
using Spine.Unity;
using System;
using TMPro;
using UnityEngine;

namespace TestGame.Scripts.Characters.Player
{
    [RequireComponent( typeof(ActorShootController))]
    public class ActorController : CharacterController
    {
        [SerializeField]
        private ActorShootController _shootController;

        public event Action<bool> On„RhooseTargetEvent;


        public override void Initialize()
        {
            _shootController.Initialize();
            _shootController.On„RhoosingTargetEvent += On„RhooseTarget;
           
            IsInitialize = true;
        }

        public void OnTakeDamage()
        {
            Disable();
        }
        public void Shoot()
        {
            _shootController.Shoot();
        }

        public void Disable()
        {
            CanMove = false;
            _shootController.Enabled = false;
        }
        public void Enable()
        {
            CanMove = true;
            _shootController.Enabled = true;
        }

        protected override void CharacterMovement()
        {
            transform.Translate(Vector2.right * Time.fixedDeltaTime * _characterSpeed, Space.World);
        }
        private void On„RhooseTarget(bool targetExists)
        {
            CanMove = false;
            On„RhooseTargetEvent?.Invoke(targetExists);

        }
    }
}

