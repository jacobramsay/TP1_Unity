using Playmode.Ennemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableController : MonoBehaviour
    {
        [Header("Type Images")] [SerializeField] private Sprite MedicalKitSprite;
        [SerializeField] private Sprite UziSprite;
        [SerializeField] private Sprite ShotgunSprite;

        private EnnemyPickableSensor enemySensor;
        private PickableSpawner pickableSpawner;
        private PickableType type;

        public void Configure(PickableType type, PickableSpawner pickableSpawner)
        {
            this.pickableSpawner = pickableSpawner;
            this.type = type;
            switch (this.type)
            {
                case PickableType.MedicalKit:
                    GetComponent<SpriteRenderer>().sprite = MedicalKitSprite;
                    break;
                case PickableType.Uzi:
                    GetComponent<SpriteRenderer>().sprite = UziSprite;
                    break;
                case PickableType.Shotgun:
                    GetComponent<SpriteRenderer>().sprite = ShotgunSprite;
                    break;
            }
        }

        private void Awake()
        {
            ValidateSerialisedFields();

            enemySensor = GetComponent<EnnemyPickableSensor>();
        }

        private void OnEnable()
        {
            enemySensor.OnEnemySensed += NotifyEnemySensed;
        }

        private void OnDisable()
        {
            enemySensor.OnEnemySensed -= NotifyEnemySensed;
        }

        private void ValidateSerialisedFields()
        {
            if (MedicalKitSprite == null)
                throw new ArgumentException("Type sprites must be provided. MedicalKitSprite is missing.");
            if (UziSprite == null)
                throw new ArgumentException("Type sprites must be provided. UziSprite is missing.");
            if (ShotgunSprite == null)
                throw new ArgumentException("Type sprites must be provided. ShotgunSprite is missing.");
        }

        private void NotifyEnemySensed(EnnemyController ennemyController)
        {                     
            AffectPickableOnEnemy(ennemyController);
            pickableSpawner.ActivateSpawnPoint();
            Destroy(gameObject);
        }

        private void AffectPickableOnEnemy(EnnemyController ennemyController)
        {
            switch (type)
            {
                case PickableType.MedicalKit:
                    
                    break;
                case PickableType.Uzi:
                   
                    break;
                case PickableType.Shotgun:
                   
                    break;
            }
        }
    }
}
