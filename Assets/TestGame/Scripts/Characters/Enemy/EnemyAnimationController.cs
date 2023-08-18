using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace TestGame.Scripts.Characters.Enemy
{
    public class EnemyAnimationController : CharacterAnimationController
    {
        [SpineAnimation] readonly string _runAnimationProperty = "run";
        [SpineAnimation] readonly string _angryAnimationProperty = "angry";
        [SpineAnimation] readonly string _winAnimationProperty = "win";

        [SerializeField]
        private ParticleSystem _deathParticles;
        [SerializeField]
        private AudioSource _zombieGrowl;


        private Coroutine currentRoutine = null;


        public event Action OnAngryCompleteEvent;

        public void OnTakeDamage(Vector3 position)
        {
            var particle = Instantiate(_deathParticles);
            particle.transform.position = position;
        }
        public override void MovingAnimation()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _runAnimationProperty, true);
        }
        public void СelebrateAnimation()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _winAnimationProperty, true);
        }
        public void TriggeredAnimation()
        {
            currentRoutine = StartCoroutine(TriggeredRoutine());
        }
        /// <summary>
        /// A Coroutine that starts and waits for the end of the animation of angry.
        /// </summary>
        /// <returns></returns>
        private IEnumerator TriggeredRoutine()
        {
            var trak = _skeletonAnimation.AnimationState.SetAnimation(0, _angryAnimationProperty, false);
            _zombieGrowl.Play();
            yield return new WaitForSpineAnimationComplete(trak);

            OnAngryCompleteEvent?.Invoke();
        }
    }
}
