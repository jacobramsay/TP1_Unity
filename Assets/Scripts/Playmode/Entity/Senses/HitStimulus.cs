using Playmode.Bullet;
using Playmode.Util.Values;
using System;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class HitStimulus : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private int hitPoints = 10;
        private BulletController bullet;
   
        private void Awake()
        {
            ValidateSerializeFields();
            InitializeComponent();
        }

        private void ValidateSerializeFields()
        {
            if (hitPoints < 0)
                throw new ArgumentException("Hit points can't be less than 0.");
        }
        private void InitializeComponent()
        {
            bullet = transform.root.GetComponentInChildren<BulletController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == Tags.Enemy)
            {
                other.GetComponent<Entity.Senses.HitSensor>()?.Hit(hitPoints);
                bullet.GetComponent<Entity.Destruction.RootDestroyer>()?.Destroy();
            }                            
        }
    }
}