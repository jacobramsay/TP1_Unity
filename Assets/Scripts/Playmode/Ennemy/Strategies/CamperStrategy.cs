using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Status;
using Playmode.Movement;
using Playmode.Pickable;
using Playmode.Util.Values;
using System;
using System.Collections;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CamperStrategy : BaseStrategy
    {
        [SerializeField] private float rangeMedic = 3f;
        [SerializeField] private float minSpawnDelayInSeconds = 3f;
        [SerializeField] private float maxSpawnDelayInSeconds = 6f;
        [SerializeField] private float speedChasingMedic = 15f;
        private bool IsChasingGun;
        private bool IsChasingMedic;
        private GameObject targetEnnemy;
        Health health;


        public CamperStrategy(Mover mover, HandController handController) : base(mover, handController)
        {
            health = mover.transform.GetComponent<Health>();
        }

        public override void Act()
        {
            if(IsChasingGun)
            {
                if(target != null && target.activeSelf)
                {
                    moverDirection = GetDirectionToTarget();
                    UpdateRotationToTarget();
                    mover.Move(Vector3.up);
                             
                }
                else
                {
                    StartSearching();
                    StopChasingGun();
                }

            }
            else if (IsChasingMedic)
            {
                if (target != null && target.activeSelf)
                {
                    if ((IsCloseEnoughToMedicPosition(target.transform.position) == false))
                    {
                        moverDirection = GetDirectionToTarget();
                        rotationAngle = GetAngleRotation(target.transform.position);
                        if (Mathf.Abs(rotationAngle) > 0)
                        {
                            mover.Rotate(rotationAngle);
                        }
                        mover.Move(Vector3.up*speedChasingMedic);
                    }

                    if(health.HealthPoints<=25)
                    {
                        moverDirection = GetDirectionToTarget();
                        rotationAngle = GetAngleRotation(target.transform.position);
                        if (Mathf.Abs(rotationAngle) > 0)
                        {
                            mover.Rotate(rotationAngle);
                        }
                        mover.Move(Vector3.up*speedChasingMedic);
                    }

                    if(targetEnnemy==null)
                    {
                        mover.ActivateScanTarget();
                    }

                    else if(targetEnnemy != null && targetEnnemy.CompareTag(Tags.Enemy))
                    {
                        
                        moverDirection = GetDirectionToTargetEnnemy();
                        rotationAngle = GetAngleRotation(targetEnnemy.transform.position);
                        if (Mathf.Abs(rotationAngle) > 0)
                        {
                            mover.Rotate(rotationAngle);
                        }
                        handController.Use();
                    }
                }
                else
                {
                    StartSearching();
                    StopChasingMedic();
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

        public override void UpdateTarget(GameObject targetEnnemy)
        {
            if ((!IsChasing && !IsChasingGun &&!IsChasingMedic) || (IsChasingMedic && (IsCloseEnoughToMedicPosition(target.transform.position)) && target != null))
            {
                this.targetEnnemy = targetEnnemy;
            }
        }

        public override void PickableDetected(PickableController pickable)
        {
            if (pickable!=null)
            {            
                if (pickable.GetPickableType() == PickableType.MedicalKit)
                {
                    target = pickable.gameObject;
                    StartChasingMedic();
                    IsSearching = false;
                }
                else if (pickable.GetPickableType() == PickableType.Shotgun || pickable.GetPickableType() == PickableType.Uzi)
                {
                    target = pickable.gameObject;
                    StartChasingGun();
                    StopChasingMedic();
                    IsSearching = false;
                }
            }
        }

        private void StartChasingGun()
        {
            IsChasingGun = true;
        }

        private void StopChasingGun()
        {
            IsChasingGun = false;
        }

        private void StartChasingMedic()
        {
            IsChasingMedic = true;
        }
        private void StopChasingMedic()
        {
            IsChasingMedic = false;
        }

        protected Vector3 GetDirectionToTargetEnnemy()
        {
            Vector3 targetPosition = targetEnnemy.transform.position;
            Vector3 currentPosition = mover.transform.position;
            return targetPosition - currentPosition;
        }

        private bool IsCloseEnoughToMedicPosition(Vector3 target)
        {
            float distance = GetDistanceBetweenTargetPosition(target);
            return distance <= rangeMedic;
        }
    }
}





