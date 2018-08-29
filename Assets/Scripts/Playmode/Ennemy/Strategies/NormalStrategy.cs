using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using UnityEngine;
using Playmode.Util.Values;
using Assets.Scripts.Playmode.Util.Values;

namespace Playmode.Ennemy.Strategies
{
    public class NormalStrategy : BaseStrategy
    {

        public NormalStrategy(Mover mover, HandController handController)
        {         
            IsChasing = false;
            IsSearching = true;
            this.mover = mover;
            this.handController = handController;
        }
        public void UpdateTarget(EnnemyController ennemy)
        {
            if(!IsChasing)
            {
                target = ennemy;
                IsChasing = true;
                IsSearching = false;
            }            
        }
        public void StopChasing()
        {
            IsChasing = false;
        }
        public void StartChasing()
        {
            IsChasing = true;
        }

        public void StartSearching()
        {
            IsSearching = true;
        }

        public void Act()
        {
            if(IsChasing)
            {
                if (target != null)
                {                 
                    Debug.DrawLine(mover.transform.position, target.transform.position);
                    moverDirection = GetDirectionToTarget();
                    rotationAngle = GetAngleRotation(target.transform.position);
                    if (Mathf.Abs(rotationAngle) > 0 )
                    {
                        mover.Rotate(rotationAngle);
                    }

                    if (!IsCloseEnoughToTargetPosition(target.transform.position))
                    {
                        mover.Move(Vector3.up);
                    }
                    else
                    {
                        handController.Use();
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
      
       
        
    }
}
