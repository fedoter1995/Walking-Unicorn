using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Scripts.Characters.Player;
using UnityEngine;

namespace TestGame.Scripts.Locations
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class LocationTransition : MonoBehaviour
    {

        [SerializeField]
        private LocationSettings _locationSettings;

        public event Action<LocationSettings> OnTransitionEvent;

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.GetComponent<Actor>();

            if (player)
            {
                OnTransitionEvent?.Invoke(_locationSettings);
            }
        }
    }
}
