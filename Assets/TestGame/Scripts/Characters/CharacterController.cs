using System;
using UnityEngine;

namespace TestGame.Scripts.Characters
{
    public abstract class CharacterController : MonoBehaviour
    {

        [SerializeField]
        protected float _characterSpeed = 1f;

        private bool canMove = false;

        public event Action OnStartMovingEvent;
        public bool IsInitialize { get; protected set; } = false;
        public bool CanMove
        {
            get
            {
                return canMove;
            }
            set
            {
                canMove = value;
            }
        }

        public abstract void Initialize();
        public virtual void StartMoving()
        {
            CanMove = true;
            OnStartMovingEvent?.Invoke();
        }
        private void FixedUpdate()
        {
            if (IsInitialize && CanMove)
            {
                CharacterMovement();
            }
        }
        protected abstract void CharacterMovement();
    }
}
