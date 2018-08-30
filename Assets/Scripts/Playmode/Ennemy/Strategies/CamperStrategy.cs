using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CamperStrategy : BaseStrategy
    {
        [SerializeField] private float rangeMedic = 3f;
        private bool IsChasingGun=false;
        private bool IsChasingMedic=false;
        private GameObject targetEnnemy;


        public CamperStrategy(Mover mover, HandController handController) : base(mover, handController)
        {
            IsChasingGun = false;
            IsChasingMedic = false;
        }
        public override void Act()
        {
            if (IsChasingMedic)
            {
                if (target != null)
                {
                    if (IsCloseEnoughToMedicPosition(target.transform.position) == false)
                    {
                        moverDirection = GetDirectionToTarget();
                        rotationAngle = GetAngleRotation(target.transform.position);
                        if (Mathf.Abs(rotationAngle) > 0)
                        {
                            mover.Rotate(rotationAngle);
                        }
                        mover.Move(Vector3.up);
                        Debug.Log("Jai trouver un medic et javance dessus");
                    }

                    else if (IsCloseEnoughToMedicPosition(target.transform.position))
                    {
                        //StopChasingMedic();
                        StopChasing();
                        if(targetEnnemy==null)
                        {
                            UpdateTarget(targetEnnemy);
                            Debug.Log("Jai trouver une cible");
                        }
                        if(targetEnnemy!=null)
                        {
                            moverDirection = GetDirectionToTargetEnnemy();
                            rotationAngle = GetAngleRotation(targetEnnemy.transform.position);
                            if (Mathf.Abs(rotationAngle) > 0)
                            {
                                mover.Rotate(rotationAngle);
                            }
                            handController.Use();
                            Debug.Log("Jai trouver un medic, je commence a tirer sur les ennemis");
                        }

                                                              
                    }
                    else
                    {
                        StopChasing();
                        StartSearching();
                    }
                }
            }

            else if(IsChasingGun)
            {
                if(target!=null)
                {
                    moverDirection = GetDirectionToTarget();
                    rotationAngle = GetAngleRotation(target.transform.position);
                    if (Mathf.Abs(rotationAngle) > 0)
                    {
                        mover.Rotate(rotationAngle);
                    }
                    mover.Move(Vector3.up);
                    Debug.Log("Jai trouver une armeeeeee et javance dessus");               
                }
                else
                {
                    StopChasingGun();
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

        public override void UpdateTarget(GameObject targetEnnemy)
        {
            if (!IsChasing && !IsChasingGun && !IsChasingMedic)
            {
                this.targetEnnemy = targetEnnemy;
                IsChasing = true;
                IsSearching = false;
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
                    StopChasing();
                }
                else if (pickable.GetPickableType() == PickableType.Shotgun || pickable.GetPickableType() == PickableType.Uzi)
                {
                    target = pickable.gameObject;
                    StartChasingGun();
                    StopChasing();
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





