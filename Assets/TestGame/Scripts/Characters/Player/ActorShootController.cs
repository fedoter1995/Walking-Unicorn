using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Scripts.Characters.Enemy;
using TestGame.Scripts.Projectiles;
using UnityEngine;

namespace TestGame.Scripts.Characters.Player
{
    public class ActorShootController : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Projectile _projectilePrefab;
        [SerializeField]
        private float _projectileSpeed = 10f;
        [SerializeField, Tooltip("The place where the projectile takes off from")]
        private Transform _shootPoint;
        [SerializeField]
        private List<TakeDamageType> _typesCanBeDamaged;


        private Pool<Projectile> _projPool;

        private ITakeDamage target;

        public event Action<bool> OnСhoosingTargetEvent;

        public bool Enabled { get; set; } = false;
        public void Initialize()
        {
            if (_projectilePrefab == null)
                throw new Exception("Add the projectile prefab");

            if (_camera == null)
                _camera = Camera.main;


            var poolParent = new GameObject($"{this}__PROJECTILE CONTAINER__");

            _projPool = new Pool<Projectile>(_projectilePrefab,5, poolParent.transform,true);
        }

        /// <summary>
        /// We take a free projectile from the pool and launch it at the enemy.
        /// </summary>
        public void Shoot()
        {
            var proj = _projPool.GetFreeObject();
            proj.transform.position = _shootPoint.position;
            proj.Initialize(target, _projectileSpeed);
            target = null;
        }
        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && Enabled)
            {
                Raycast();
            }
            
        }
        /// <summary>     
        /// We let the ray, and if he got into a suitable target, we call OnСhoosingTargetEvent with true
        /// else with false
        /// </summary>
        private void Raycast()
        {
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                var target = hit.collider.gameObject.GetComponent<ITakeDamage>();

                if (target != null && _typesCanBeDamaged.Contains(target.TakeDamageType))
                {
                    this.target = target;
                    OnСhoosingTargetEvent?.Invoke(true);
                }
                else
                    OnСhoosingTargetEvent?.Invoke(false);
            }
            else
            {
                OnСhoosingTargetEvent?.Invoke(false);
            }

        }
    }
}
