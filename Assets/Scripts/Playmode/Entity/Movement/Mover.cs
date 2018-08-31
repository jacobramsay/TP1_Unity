using System;
using System.Collections;
using UnityEngine;

namespace Playmode.Movement
{
    public abstract class Mover : MonoBehaviour
    {
        public static readonly Vector3 Foward = Vector3.up;
        public const float Clockwise = 1f;

        [SerializeField] protected float speed = 1.75f;
        [SerializeField] protected float rotateSpeed = 90f;
        [SerializeField] private float minScanDelayInSeconds = 1f;
        [SerializeField] private float maxScanDelayInSeconds = 3f;
        [SerializeField] private float currCountdownValue;
        public bool isInvincible;

        public void ActivateScanTarget()
        {
            StartCoroutine(SpawnPrefabsRoutine());
        }

        public void ActivateCountdown()
        {
            StartCoroutine(InvicibilityRoutine(5));
        }

        private void OnEnable()
        {
            ActivateScanTarget();
            ActivateCountdown();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnPrefabsRoutine()
        {
            var rdmSpawnDelayInSeconds = UnityEngine.Random.Range(minScanDelayInSeconds, maxScanDelayInSeconds);
            yield return new WaitForSeconds(rdmSpawnDelayInSeconds);
            RandomAngleRotation();
        }

        private IEnumerator InvicibilityRoutine(float countdownValue = 5)
        {
            isInvincible = true;
            yield return new WaitForSeconds(countdownValue);
            isInvincible = false;
        }

        private void RandomAngleRotation()
        {
            var rdmAngle = UnityEngine.Random.Range(10f, 500f);
            RotateCamper(rdmAngle);
        }

        private void RotateCamper(float angle)
        {
            Rotate(angle);
        }


        protected void Awake()
        {
            ValidateSerialisedFields();
        }

        private void ValidateSerialisedFields()
        {
            if (speed < 0)
                throw new ArgumentException("Speed can't be lower than 0.");
            if (rotateSpeed < 0)
                throw new ArgumentException("RotateSpeed can't be lower than 0.");
        }

        public abstract void Move(Vector3 direction);

        public abstract void Rotate(float direction);
    }
}