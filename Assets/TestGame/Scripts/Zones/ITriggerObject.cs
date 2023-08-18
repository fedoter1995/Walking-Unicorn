using UnityEngine;

namespace SpaceTraveler.GameStructures.Zones
{
    public interface ITriggerObject
    {
        TriggerObjectType TriggerType { get; }
        Vector3 Position { get; }
    }
}
