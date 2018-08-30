using Assets.Scripts.Playmode.Weapon;
using System;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireDelayInSeconds = 2f;
        [SerializeField] private float weaponDamage = 1f;

        private float lastTimeShotInSeconds;
        private WeaponType type;

        private bool CanShoot => Time.time - lastTimeShotInSeconds > fireDelayInSeconds;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
            type = WeaponType.Normal;
        }

        public void Configure( WeaponType type)
        {
            switch (this.type)
            {
                case WeaponType.Uzi:
                   // GetComponent<SpriteRenderer>().sprite = UziSprite;
                   if(this.type == type)
                    {
                        fireDelayInSeconds = fireDelayInSeconds / 2;
                    }
                   else
                    {
                        this.type = type;
                        fireDelayInSeconds = 1f;
                    }
                    break;
                case WeaponType.Shotgun:
                    // GetComponent<SpriteRenderer>().sprite = ShotgunSprite;
                    if (this.type == type)
                    {
                        weaponDamage += weaponDamage / 2;
                    }
                    else
                    {
                        this.type =type;
                        fireDelayInSeconds = 5f;
                    }
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
                    var baseAngle = transform.rotation;

                    Instantiate(bulletPrefab, transform.position, transform.rotation);

                    transform.Rotate(Vector3.forward, -10);
                    Instantiate(bulletPrefab, transform.position, transform.rotation);

                    transform.Rotate(Vector3.forward,5);
                    Instantiate(bulletPrefab, transform.position, transform.rotation);

                    transform.Rotate(Vector3.forward, 10);
                    Instantiate(bulletPrefab, transform.position, transform.rotation);

                    transform.Rotate(Vector3.forward, 5);
                    Instantiate(bulletPrefab, transform.position, transform.rotation);

                    transform.rotation = baseAngle;
                    
                    lastTimeShotInSeconds = Time.time;
                }
                else
                {
                    Instantiate(bulletPrefab, transform.position, transform.rotation);
                    lastTimeShotInSeconds = Time.time;
                }
                
            }
        }
    }
}