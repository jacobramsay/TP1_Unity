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
                switch(pickable.GetPickableType())
                {
                    case (PickableType.MedicalKit):                       
                        other.GetComponent<Entity.Senses.BonusSensor>()?.Heal(healPoints);
                        break;
                    case (PickableType.Shotgun):
                        other.GetComponent<Entity.Senses.BonusSensor>()?.NewWeapon(WeaponType.Shotgun);
                        break;
                    case (PickableType.Uzi):
                        other.GetComponent<Entity.Senses.BonusSensor>()?.NewWeapon(WeaponType.Uzi);
                        break;
                }               
            }
        }
    }
}