using CustomTools;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace TestGame.Scripts
{
    [Serializable]
    public class GlobalVolumeHandler
    {
        [SerializeField]
        private Transform _parent;
        [SerializeField]
        private Volume _volumePrefab;
        [SerializeField]
        private float _fadeDuration;



        private Pool<Volume> _volumesPool;

        private Volume currentVolume;

        public void Initialize()
        {
            //Creating a pool of "Volume" for a smooth change of Global Volume Profile
            _volumesPool = new Pool<Volume>(_volumePrefab, 2, _parent, false);

            currentVolume = _volumesPool.GetFreeObject();
        }


        /// <summary>
        /// Smooth change Postproceesing Volume profile
        /// </summary>
        /// <param name="volumeProfile">New Volume profile</param>
        public void SmoothlyChangeVolume(VolumeProfile volumeProfile)
        {
            var newVolume = _volumesPool.GetFreeObject();
            newVolume.weight = 0;
            newVolume.profile = volumeProfile;


            DOVirtual.Float(currentVolume.weight, 0f, _fadeDuration, value => SetWeight(currentVolume, value));
            DOVirtual.Float(newVolume.weight, 1f, _fadeDuration, value => SetWeight(newVolume, value)).OnComplete(() =>
            {
                currentVolume.gameObject.SetActive(false);
                currentVolume = newVolume;

            });

        }
        private void SetWeight(Volume volume, float value)
        {
            volume.weight = value;
        }

    }
}
