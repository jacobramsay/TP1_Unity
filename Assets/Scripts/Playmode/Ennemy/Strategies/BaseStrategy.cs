using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
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
        protected EnnemyController target;
        protected float rotationAngle;
        protected Vector3 moverDirection;
        public bool IsChasing { get; set; }
        public bool IsSearching { get; set; }


        public void Act()
        {
            throw new NotImplementedException();
        }

        public void UpdateTarget(EnnemyController ennemy)
        {
            throw new NotImplementedException();
        }
        private float GetAngleRotation(Vector3 targetPosition)
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

        private Vector3 GetDirectionToTarget()
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 currentPosition = mover.transform.position;
            return targetPosition - currentPosition;
        }

        private Vector3 GetRandomDirection()
        {
            var randomX = UnityEngine.Random.Range(Const.MIN_GAME_WIDTH, Const.MAX_GAME_WIDTH);
            var randomY = UnityEngine.Random.Range(Const.MIN_GAME_HEIGHT, Const.MAX_GAME_HEIGHT);

            return new Vector3(randomX, randomY, 0) - mover.transform.position;
        }

        private float GetDistanceBetweenTargetPosition(Vector3 target)
        {
            return Vector3.Distance(mover.transform.position, target);
        }

        private bool IsCloseEnoughToTargetPosition(Vector3 target)
        {
            float distance = GetDistanceBetweenTargetPosition(target);
            return distance <= range;
        }
    }
}
