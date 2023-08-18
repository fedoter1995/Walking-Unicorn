using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Zones
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField]
        protected Collider2D triggerCollider;
        
        
        [SerializeField,Tooltip("Types That the object Reacts To")]
        protected List<TriggerObjectType> _correctTypes = new List<TriggerObjectType>();



        protected List<ITriggerObject> inZoneObjects = new List<ITriggerObject>();


        public event Action<ITriggerObject> OnAddObjectEvent;

        public List<ITriggerObject> InZoneObjects => inZoneObjects;
        public List<TriggerObjectType> CorrectTypes => _correctTypes;

        public bool HaveObjectInZone(ITriggerObject obj)
        {
            return inZoneObjects.Contains(obj);
        }

        protected virtual void AddObject(ITriggerObject obj)
        {

            if (inZoneObjects.Contains(obj))
                return;

            inZoneObjects.Add(obj);

            OnAddObjectEvent?.Invoke(obj);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            var obj = collision.GetComponent<ITriggerObject>();
            if (obj == null)
                obj = collision.GetComponentInParent<ITriggerObject>();

            if (_correctTypes.Count > 0)
            {
                if (obj != null && CorrectTypes.Contains(obj.TriggerType))
                    AddObject(obj);
            }
            else
            {
                if (obj != null)
                {
                    AddObject(obj);
                }
            }

        }

    }

    public enum TriggerObjectType
    {
        Enemy,
        Player
    }
}