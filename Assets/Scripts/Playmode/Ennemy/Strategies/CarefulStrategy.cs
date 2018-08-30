using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Status;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CarefulStrategy : BaseStrategy
    {
        Health health;
        public CarefulStrategy(Mover mover, HandController handController):base(mover,handController)
        {
            health = mover.transform.GetComponent<Health>();
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
                    else
                    {
                        mover.Move(Vector3.up);
                    }
                    if(NeedsMedicalKit())
                    {
                        StopChasing();
                        StartSearching();
                    }
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
        public override void PickableDetected(PickableController pickable)
        {
            
        }
        private bool NeedsMedicalKit()
        {
            return health.HealthPoints <= 25;
        }
    }
}
