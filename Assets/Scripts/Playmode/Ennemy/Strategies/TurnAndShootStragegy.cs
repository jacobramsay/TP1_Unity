using Playmode.Ennemy.BodyParts;
using Playmode.Movement;

namespace Playmode.Ennemy.Strategies
{
    public class TurnAndShootStragegy : BaseStrategy
    {
    
        public TurnAndShootStragegy(Mover mover, HandController handController):base(mover,handController)
        {
           
        }     
        public override void Act()
        {
            mover.Rotate(Mover.Clockwise);

            handController.Use();
        }
    }
}