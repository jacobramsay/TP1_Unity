using Assets.Scripts.Playmode.Weapon;
using Playmode.Pickable;
using Playmode.Util.Values;
using System;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class BonusStimulus : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private int healPoints = 30;        
        private PickableController pickable;
        private void Awake()
        {
            ValidateSerializeFields();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            pickable = transform.root.GetComponentInChildren<PickableController>();
        }

        private void ValidateSerializeFields()
        {
            if (healPoints < 0)
                throw new ArgumentException("Heal points can't be less than 0.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Tags.Enemy)
            {
                AffectBonus(other);
            }
        }
        private void AffectBonus(Collider2D other)
        {
            switch (pickable.GetPickableType())
            {
                case (PickableType.MedicalKit):
                    AffectMedicalKit(other);
                    break;
                case (PickableType.Shotgun):
                    AffectShotgun(other);
                    break;
                case (PickableType.Uzi):
                    AffectUzi(other);
                    break;
                case (PickableType.Invincible):
                    AffectInvincible(other);
                    break;
            }
        }
        private void AffectMedicalKit(Collider2D other)
        {
            other.GetComponent<Entity.Senses.BonusSensor>()?.Heal(healPoints);
        }
        private void AffectShotgun(Collider2D other)
        {
            other.GetComponent<Entity.Senses.BonusSensor>()?.NewWeapon(WeaponType.Shotgun);
        }
        private void AffectUzi(Collider2D other)
        {
            other.GetComponent<Entity.Senses.BonusSensor>()?.NewWeapon(WeaponType.Uzi);
        }
        private void AffectInvincible(Collider2D other)
        {
            other.GetComponent<Entity.Senses.BonusSensor>()?.Invincible();
        }
    }
}