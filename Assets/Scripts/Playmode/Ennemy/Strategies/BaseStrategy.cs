using Assets.Scripts.Playmode.Util.Values;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Pickable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class BaseStrategy : IEnnemyStrategy
    {
        [SerializeField] protected float range = 7f;
        protected readonly Mover mover;
        protected readonly HandController handController;
        protected GameObject target;
        protected float rotationAngle;
        protected Vector3 moverDirection;
        public bool IsChasing { get; set; }
        public bool IsSearching { get; set; }        
        public BaseStrategy(Mover mover, HandController handController)
        {
            this.mover = mover;
            this.handController = handController;
            IsChasing = false;
            IsSearching = true;

        }

        public virtual void  Act()
        {
            throw new NotImplementedException();
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

        public virtual void UpdateTarget(GameObject target)
        {
            if (!IsChasing)
            {
                this.target = target;
                IsChasing = true;
                IsSearching = false;
            }
        }

        public virtual void PickableDetected(PickableController pickable)
        {
        }

        protected float GetAngleRotation(Vector3 targetPosition)
        {
            Vector3 currentPosition = mover.transform.position;
            float rotationAngle = Vector3.Angle(currentPosition, targetPosition);
            Vector3 directionToTarget = targetPosition - currentPosition;
            float dotProductResult = Vector3.Dot(directionToTarget, mover.transform.right);
            if (dotProductResult > 0.1f)
            {
                return rotationAngle;
            }
            else if (dotProductResult < 0.1f)
            {
                return -rotationAngle;
            }
            else
            {
                return 0;
            }
        }

        protected Vector3 GetDirectionToTarget()
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 currentPosition = mover.transform.position;
            return targetPosition - currentPosition;
        }

        protected Vector3 GetRandomDirection()
        {
            var randomX = UnityEngine.Random.Range(Const.MIN_GAME_WIDTH, Const.MAX_GAME_WIDTH);
            var randomY = UnityEngine.Random.Range(Const.MIN_GAME_HEIGHT, Const.MAX_GAME_HEIGHT);

            return new Vector3(randomX, randomY, 0) - mover.transform.position;
        }

        protected float GetDistanceBetweenTargetPosition(Vector3 target)
        {
            return Vector3.Distance(mover.transform.position, target);
        }

        protected bool IsCloseEnoughToTargetPosition(Vector3 target)
        {
            float distance = GetDistanceBetweenTargetPosition(target);
            return distance <= range;
        }        
    }
}
