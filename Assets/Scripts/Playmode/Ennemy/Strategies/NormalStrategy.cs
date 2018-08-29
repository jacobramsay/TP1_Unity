using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using UnityEngine;
using Playmode.Util.Values;
using Assets.Scripts.Playmode.Util.Values;

namespace Playmode.Ennemy.Strategies
{
    public class NormalStrategy : BaseStrategy
    {

        public NormalStrategy(Mover mover, HandController handController): base(mover,handController)
        {         
                      
        }              
        public override void Act()
        {
            if(IsChasing)
            {
                if (target != null)
                {                                     
                    moverDirection = GetDirectionToTarget();
                    rotationAngle = GetAngleRotation(target.transform.position);
                    if (Mathf.Abs(rotationAngle) > 0 )
                    {
                        mover.Rotate(rotationAngle);
                    }

                    if (IsCloseEnoughToTargetPosition(target.transform.position))
                    {
                        handController.Use();
                    }                    
                    mover.Move(Vector3.up);

                }
                else
                {
                    StopChasing();
                    StartSearching();

                }
            }
            else if (IsSearching)
            {
                moverDirection = GetRandomDirection();
                rotationAngle = GetAngleRotation(moverDirection);
                if (rotationAngle > 2 || rotationAngle < -2)
                {
                    mover.Rotate(rotationAngle);
                }
                mover.Move(Vector3.up);
            }
        }
      
       
        
    }
}
