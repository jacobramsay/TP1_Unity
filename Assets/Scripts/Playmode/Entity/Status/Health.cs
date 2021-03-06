﻿using System;
using UnityEngine;
using UnityEngine.UI;
namespace Playmode.Entity.Status
{
    public delegate void HealthEventHandler();

    public class Health : MonoBehaviour
    {
        [SerializeField] private int healthPoints = 100;
        public Slider HealthBar;
        public event HealthEventHandler OnDeath;

        public int HealthPoints
        {
            get { return healthPoints; }
            private set
            {
                healthPoints = value < 0 ? 0 : value;

                if (healthPoints <= 0) NotifyDeath();
            }
        }

        private void Awake()
        {
            ValidateSerialisedFields();
            HealthBar.value = HealthPoints;
        }
        private void Update()
        {
            UpdateHealthBarValue();
        }
        private void ValidateSerialisedFields()
        {
            if (healthPoints < 0)
                throw new ArgumentException("HealthPoints can't be lower than 0.");
        }
        public void Heal(int healPoints)
        {
            HealthPoints += healPoints;
        }
        public void Hit(int hitPoints)
        {
            HealthPoints -= hitPoints;
        }

        private void NotifyDeath()
        {
            if (OnDeath != null) OnDeath();
        }
        private void UpdateHealthBarValue()
        {
            HealthBar.value = HealthPoints;
        }
    }
}