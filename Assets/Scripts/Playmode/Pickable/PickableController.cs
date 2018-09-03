using Assets.Scripts.Playmode.Weapon;
using Playmode.Ennemy;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Destruction;
using Playmode.Weapon;
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
        [SerializeField] private Sprite InvincibleSprite;
        private EnnemyPickableSensor enemySensor;
        private PickableSpawner pickableSpawner;
        private PickableType type;
        private Destroyer destroyer;

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
                case PickableType.Invincible:
                    GetComponent<SpriteRenderer>().sprite = InvincibleSprite;
                    break;
            }
        }

        public PickableType GetPickableType()
        {
            return type;
        }
        private void Awake()
        {
            ValidateSerialisedFields();

            enemySensor = GetComponent<EnnemyPickableSensor>();
            destroyer = GetComponent<RootDestroyer>();
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
            if (InvincibleSprite == null)
                throw new ArgumentException("Type sprites must be provided. InvincibleSprite is missing.");
        }

        private void NotifyEnemySensed(EnnemyController ennemyController)
        {                     
            AffectPickableOnEnemy(ennemyController);
            pickableSpawner.ActivateSpawnPoint();
            destroyer.Destroy();
        }

        private void AffectPickableOnEnemy(EnnemyController ennemyController)
        {

            var rootTransform = transform.root;
            var weaponController = rootTransform.GetComponentInChildren<WeaponController>();


            switch (type)
            {
                case PickableType.MedicalKit:
                    
                    break;
                case PickableType.Uzi:
                    // weaponController.Configure(WeaponType.Uzi);
                   // ennemyController.NewWeapon(WeaponType.Uzi);
                    break;
                case PickableType.Shotgun:
                    //  weaponController.Configure(WeaponType.Shotgun);
                   // ennemyController.NewWeapon(WeaponType.Shotgun);
                    break;
                case PickableType.Invincible:
                    break;
            }
        }
    }
}
