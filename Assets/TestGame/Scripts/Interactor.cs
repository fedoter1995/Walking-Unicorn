using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Scripts.Characters.Player;
using UnityEngine;

namespace TestGame.Scripts
{
    [RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
    public class Interactor : MonoBehaviour
    {
        public event Action OnInteractEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.GetComponent<Actor>();

            if(player)
            {
                OnInteractEvent?.Invoke();
            }
        }
    }
}
