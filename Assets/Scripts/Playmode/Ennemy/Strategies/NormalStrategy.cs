using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class NormalStrategy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private EnnemyController target;        
        private float rotationAngle;      
        public bool IsChasing { get; set; }
        public NormalStrategy(Mover mover, HandController handController)
        {         
            IsChasing = false;
            this.mover = mover;
            this.handController = handController;
        }
        public void UpdateTarget(EnnemyController ennemy)
        {
            if(!IsChasing)
            {
                target = ennemy;
                IsChasing = true;
            }            
        }
        public void StopChasing()
        {
            IsChasing = false;
        }
        public void Act()
        {
            if(IsChasing)
            {
                rotationAngle = GetAngleRotation();                      
                if (rotationAngle > 2 || rotationAngle < -2 )
                {
                   mover.Rotate(rotationAngle);
                }                
                mover.Move((target.transform.position-mover.transform.position));
                handController.Use();               
            }                                 
           
        }
        private float GetAngleRotation()
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 currentPosition = mover.transform.position;
            float rotationAngle = Vector3.Angle(currentPosition, targetPosition);
            Vector3 directionToTarget = targetPosition - currentPosition;
            float dotProductResult = Vector3.Dot(directionToTarget, mover.transform.right);          
            if(dotProductResult > 1)
            {
                return rotationAngle;
            }
            else if(dotProductResult < -1)
            {
                return  -rotationAngle;
            }
            else
            {
                return 0;
            }
        }
        private Vector3 GetDirectionToTarget()
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 currentPosition = mover.transform.position;
            return targetPosition - currentPosition;
        }
        
    }
}
