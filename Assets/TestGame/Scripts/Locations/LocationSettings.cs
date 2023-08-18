using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Scripts.Characters.Player;
using UnityEngine;
using UnityEngine.Rendering;
namespace TestGame.Scripts.Locations
{
    [Serializable]
    public class LocationSettings
    {

        [SerializeField]
        private string _locatonName;
        [SerializeField]
        private AudioClip _mainTheme;
        [SerializeField]
        private VolumeProfile _locatonVolumeProfile;

        public string LocationName => _locatonName;
        public AudioClip AudioClip => _mainTheme;
        public VolumeProfile LocatonVolumeProfile => _locatonVolumeProfile;



    }
}

