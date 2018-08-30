using Playmode.Pickable;
using System.Numerics;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public interface IEnnemyStrategy
    {
      
        void Act();
        void UpdateTarget(GameObject target);

        void PickableDetected(PickableController pickable);
    }

    public enum EnnemyStrategy
    {
        Normal,
        Careful,
        Cowboy,
        Camper
    }
}