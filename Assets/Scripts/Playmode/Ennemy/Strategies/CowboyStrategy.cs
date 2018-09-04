using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Status;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CowboyStrategy : BaseStrategy
    {
        private bool IsChasingPickable;
        private bool isCareful;
        Health health;
        public CowboyStrategy(Mover mover, HandController handController,bool isCareful) : base(mover, handController)
        {
            this.isCareful = isCareful;
            health = mover.transform.GetComponent<Health>();
        }

        public override void Act()
        {           
            if(IsChasingPickable)
            {
                if (target != null && target.activeSelf)
                {
                    moverDirection = GetDirectionToTarget();
                    UpdateRotationToTarget();
                    mover.Move(Vector3.up);
                }
                else
                {
                    StopChasingPickable();
                    StartSearching();
                }
            }
            else if (IsChasing)
            {
                if (target != null && target.activeSelf)
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
                }
                else
                {
                    StopChasing();
                    StartSearching();
                }
            }
            else if(IsSearching)
            {
                moverDirection = GetRandomDirection();
                rotationAngle = GetAngleRotation(moverDirection);
                if (rotationAngle > 2 || rotationAngle < -2)
                {
                    mover.Rotate(rotationAngle);
                }
                mover.Move(Vector3.up);
            }
            if( isCareful && HasNotEnoughHealthForCowboy())
            {
                BecomeCareful();
            }
        }

        public override void UpdateTarget(GameObject target)
        {
            if (!IsChasing && !IsChasingPickable)
            {
                this.target = target;
                IsChasing = true;
                IsSearching = false;
            }
        }

        public override void PickableDetected(PickableController pickable)
        {
            if(pickable != null)
            {
                if (pickable.GetPickableType() == PickableType.Shotgun ||
               pickable.GetPickableType() == PickableType.Uzi)
                {
                    target = pickable.gameObject;
                    StartChasingPickable();
                    StopChasing();
                }
            }
        }

        private void StartChasingPickable()
        {
            IsChasingPickable = true;
        }
        private void StopChasingPickable()
        {
            IsChasingPickable = false;
        }
        private bool HasNotEnoughHealthForCowboy()
        {
            return health.HealthPoints < 100;
        }
        private void BecomeCareful()
        {
            AssignNewStrategy(new CarefulStrategy(mover, handController));
        }
    }
}
