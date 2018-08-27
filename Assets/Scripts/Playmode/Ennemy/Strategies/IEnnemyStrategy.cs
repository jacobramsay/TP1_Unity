using System.Numerics;

namespace Playmode.Ennemy.Strategies
{
    public interface IEnnemyStrategy
    {
      
        void Act();
        void UpdateTarget(EnnemyController ennemy);       
    }

    public enum EnnemyStrategy
    {
        Normal,
        Careful,
        Cowboy,
        Camper
    }
}