using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CamperStrategy : BaseStrategy
    {      
        public CamperStrategy(Mover mover, HandController handController):base(mover,handController)
        {

        }
        public override void Act()
        {

        }
        public override void PickableDetected(PickableController pickable)
        {
            if(!IsChasing)
            {
                if(pickable.GetPickableType()==PickableType.Uzi || pickable.GetPickableType()==PickableType.Shotgun)
                {
                    this.target = target;
                    IsChasing = true;
                    IsSearching = false;
                }
            }
        }
    }
}

    
    

