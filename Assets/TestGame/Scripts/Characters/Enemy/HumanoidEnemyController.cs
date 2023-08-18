using SpaceTraveler.GameStructures.Zones;
using System;
using UnityEngine;

namespace TestGame.Scripts.Characters.Enemy
{
    public class HumanoidEnemyController : CharacterController
    {
        [SerializeField]
        private TriggerZone _triggerZone;

        private ITriggerObject target;


        public event Action OnSetTargetEvent;

        public override void Initialize()
        {
            if (_triggerZone == null)
                throw new Exception("Add TriggerZone component in inspector!");

            _triggerZone.OnAddObjectEvent += OnSetSetTarget;

            IsInitialize = true;
        }
        /// <summary>
        /// Called when a suitable target is encountered
        /// </summary>
        /// <param name="target"></param>
        private void OnSetSetTarget(ITriggerObject target)
        {
            this.target = target;

            OnSetTargetEvent?.Invoke();
        }

        protected override void CharacterMovement()
        {
            transform.Translate(Vector2.left * Time.fixedDeltaTime * _characterSpeed, Space.World);
        }
    }
}
