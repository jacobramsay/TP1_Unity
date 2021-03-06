﻿using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Status;
using Playmode.Movement;
using Playmode.Pickable;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CarefulStrategy : BaseStrategy
    {
        Health health;
        
        public CarefulStrategy(Mover mover, HandController handController) : base(mover, handController)
        {
            health = mover.transform.GetComponent<Health>();            
            range *= 2;           
        }
        public override void Act()
        {
            ActCareful();       
            if(HasLotOfHealth())
            {
                BecomeCowboy();
            }
        }
        public override void PickableDetected(PickableController pickable)
        {
           if(!IsChasing)
            {
                if(!NeedsMedicalKit())
                {
                    target = pickable.gameObject;
                    StartChasing();
                }
                else
                {
                    if(pickable.GetPickableType() == PickableType.MedicalKit)
                    {
                        target = pickable.gameObject;
                        StartChasing();
                    }
                }
            }
        }
        private bool NeedsMedicalKit()
        {            
            return health.HealthPoints <= 25;
        }
        private bool HasLotOfHealth()
        {
            return health.HealthPoints >= 100;
        }
        private void ActCareful()
        {
            if (IsChasing)
            {
                if (target != null && target.activeSelf)
                {
                    moverDirection = GetDirectionToTarget();
                    UpdateRotationToTarget();
                    if (target.tag == Tags.Enemy)
                    {
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
                        mover.Move(Vector3.up);
                    }
                    if (NeedsMedicalKit() && target.tag == Tags.Enemy)
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
        private void BecomeCowboy()
        {
            AssignNewStrategy(new CowboyStrategy(mover, handController, true));
        }
    }
}
