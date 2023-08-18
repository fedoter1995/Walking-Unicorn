using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Scripts.Characters;
using UnityEngine;
using UnityEngine.Windows;

namespace Assets.TestGame.Scripts.Player
{
    public class ActorAniamtionController : CharacterAnimationController
    {
        [SpineAnimation] readonly string _idleAnimationProperty = "idle";
        [SpineAnimation] readonly string _walkAnimationProperty = "walk";
        [SpineAnimation] readonly string _shootAnimationProperty = "shoot";
        [SpineAnimation] readonly string _shootFailAnimationProperty = "shoot_fail";
        [SpineAnimation] readonly string _deathAnimationProperty = "loose";

        [SerializeField]
        protected ParticleSystem _shootParticles;
        [SerializeField]
        protected AudioSource _shootSound;
        [SerializeField]
        protected float _shootTimeScale = 1f;
        private string shootAnimationEventName = "shooter/fire";

        private Coroutine currentEnumerator = null;

        public event Action OnShootEvent;
        public event Action OnEndShootAnimEvent;
        public event Action OnLooseAnimEndEvent;

        public void Initialize()
        {
            _skeletonAnimation.AnimationState.Event += OnUserDefinedEvent;
        }
        public override void MovingAnimation()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _walkAnimationProperty, true);
        }
        public void IdleAnimation()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _idleAnimationProperty, true);
        }
        /// <summary>
        /// Check if there are active continuous animations. And whether he can shoot.
        /// </summary>
        /// <param name="canShoot"></param>
        public void TryShootAnimation(bool canShoot)
        {
            if (currentEnumerator == null)
                currentEnumerator = StartCoroutine(ShootRoutine(canShoot));
        }

        public void DefeatAnimation()
        {
            StopAllCoroutines();
            _skeletonAnimation.ClearState();
            currentEnumerator = StartCoroutine(LooseRoutine());
        }
        /// <summary>
        /// A Coroutine that starts and waits for the end of the animation of loose. of defeat.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LooseRoutine()
        {
            var track = _skeletonAnimation.AnimationState.SetAnimation(0, _deathAnimationProperty, false);
            
            yield return new WaitForSpineAnimationComplete(track);
            OnLooseAnimEndEvent?.Invoke();
            currentEnumerator = null;
        }
        /// <summary>
        /// A Coroutine waiting for the end of waiting for the animation of shoot or shoot_fail.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShootRoutine(bool canShoot)
        {
            string animName = "";

            if (canShoot)
                animName = _shootAnimationProperty;
            else
                animName = _shootFailAnimationProperty;


            var track = _skeletonAnimation.AnimationState.SetAnimation(0, animName, false);
            track.TimeScale = _shootTimeScale;
            yield return new WaitForSpineAnimationComplete(track);

            OnEndShootAnimEvent?.Invoke();
            currentEnumerator = null;
        }
        /// <summary>
        /// Tracking custom animation events
        /// </summary>
        /// <param name="trackEntry">Spine Track</param>
        /// <param name="e">Triggered Event</param>
        private void OnUserDefinedEvent(Spine.TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == shootAnimationEventName)
            {
                _shootSound.Play();
                _shootParticles.gameObject.SetActive(true);
               
                OnShootEvent?.Invoke();
            }
            
        }

    }
}
