using Assets.Scripts.Playmode.Weapon;
using Playmode.Bullet;
using System;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireDelayInSeconds = 2f;        
        private float lastTimeShotInSeconds;
        private WeaponType type;

        private bool CanShoot => Time.time - lastTimeShotInSeconds > fireDelayInSeconds;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
            type = WeaponType.Normal;
        }

        public void Configure( WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Uzi:                    
                    ConfigureUzi();
                    break;
                case WeaponType.Shotgun:                  
                    ConfigureShotgun();
                    break;
            }
        }

        private void ValidateSerialisedFields()
        {
            if (fireDelayInSeconds < 0)
                throw new ArgumentException("FireRate can't be lower than 0.");
        }

        private void InitializeComponent()
        {
            lastTimeShotInSeconds = 0;
        }

        public void Shoot()
        {
            if (CanShoot)
            {
                if(type == WeaponType.Shotgun)
                {
                    ShootShotGunBullets();
                }
                else
                {
                    ShootBullet();
                }
                
            }
        }

        private void ConfigureUzi()
        {
            if (type == WeaponType.Uzi)
            {
                fireDelayInSeconds = fireDelayInSeconds / 2;
                bulletPrefab.GetComponentInChildren<BulletController>().DamageHitPoints++;
            }
            else
            {
                type = WeaponType.Uzi;
                fireDelayInSeconds = fireDelayInSeconds / 5;               
                bulletPrefab.GetComponentInChildren<BulletController>().DamageHitPoints = 5;
            }
        }
        private void ConfigureShotgun()
        {
            if (type == WeaponType.Shotgun)
            {
                bulletPrefab.GetComponentInChildren<BulletController>().DamageHitPoints += 5;             
            }
            else
            {
                bulletPrefab.GetComponentInChildren<BulletController>().DamageHitPoints = 20;
                type = WeaponType.Shotgun;
                fireDelayInSeconds = 2.5f;
            }
        }
        private void ShootShotGunBullets()
        {
            var baseAngle = transform.rotation;

            Instantiate(bulletPrefab, transform.position, transform.rotation);

            transform.Rotate(Vector3.forward, -10);
            Instantiate(bulletPrefab, transform.position, transform.rotation);

            transform.Rotate(Vector3.forward, 5);
            Instantiate(bulletPrefab, transform.position, transform.rotation);

            transform.Rotate(Vector3.forward, 10);
            Instantiate(bulletPrefab, transform.position, transform.rotation);

            transform.Rotate(Vector3.forward, 5);
            Instantiate(bulletPrefab, transform.position, transform.rotation);

            transform.rotation = baseAngle;

            lastTimeShotInSeconds = Time.time;
        }
        private void ShootBullet()
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            lastTimeShotInSeconds = Time.time;
        }
    }
}