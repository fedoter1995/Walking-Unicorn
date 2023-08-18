using System;
using UnityEngine;

namespace TestGame.Scripts.Tools
{
    [Serializable]
    public class AudioClipContainer
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private AudioClip _clip;


        public string Name => _name;
        public AudioClip Clip => _clip;
    }
}
