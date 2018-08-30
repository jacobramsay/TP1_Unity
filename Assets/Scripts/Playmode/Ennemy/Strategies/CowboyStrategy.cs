using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CowboyStrategy : BaseStrategy
    {

        public CowboyStrategy(Mover mover, HandController handController) : base(mover, handController)
        {

        }
        public override void Act()
        {
            if (IsChasing)
            {
                if (target != null)
                {
                    moverDirection = GetDirectionToTarget();
                    rotationAngle = GetAngleRotation(target.transform.position);
                    if (Mathf.Abs(rotationAngle) > 0)
                    {
                        mover.Rotate(rotationAngle);
                    }

                    if (IsCloseEnoughToTargetPosition(target.transform.position))
                    {
                        handController.Use();
                    }
                    mover.Move(Vector3.up);
                }
            }
            else
            {
                StopChasing();
                StartSearching();
            }
        }

        public override void PickableDetected(PickableController pickable)
        {
            
        }
    }
}
