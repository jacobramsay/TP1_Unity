using System.Numerics;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public interface IEnnemyStrategy
    {
      
        void Act();
        void UpdateTarget(GameObject target);       
    }

    public enum EnnemyStrategy
    {
        Normal,
        Careful,
        Cowboy,
        Camper
    }
}