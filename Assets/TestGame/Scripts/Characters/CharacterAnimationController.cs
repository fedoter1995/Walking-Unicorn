using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TestGame.Scripts.Characters
{
    public abstract class CharacterAnimationController : MonoBehaviour
    {

        [SerializeField]
        protected SkeletonAnimation _skeletonAnimation;

        public abstract void MovingAnimation();
    }
}
