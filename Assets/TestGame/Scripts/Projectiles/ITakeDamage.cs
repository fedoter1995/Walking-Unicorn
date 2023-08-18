using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TestGame.Scripts.Projectiles
{
    public interface ITakeDamage
    {
        TakeDamageType TakeDamageType { get; }
        Vector3 TargetPosition { get; }
        event Action OnTakeDamageEvent;
        void TakeDamage();
    }
    public enum TakeDamageType
    {
        None,
        Player,
        Enemy
    }
}
