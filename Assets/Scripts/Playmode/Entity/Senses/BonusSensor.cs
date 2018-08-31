using Assets.Scripts.Playmode.Weapon;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void HealSensorEventHandler(int healPoints);
    public delegate void NewWeaponSensorEventHandler(WeaponType weaponType);
    public delegate void InvincibleEventHandler();
    public class BonusSensor : MonoBehaviour
    {
        public event HealSensorEventHandler OnHeal;
        public event NewWeaponSensorEventHandler OnNewWeapon;
        public event InvincibleEventHandler OnInvincible;
        public void Heal(int healPoints)
        {
            NotifyHeal(healPoints);
        }
        public void NewWeapon(WeaponType weaponType)
        {
            NotifyNewWeapon(weaponType);
        }
        public void Invincible()
        {
            NotifyInvincible();
        }
        private void NotifyHeal(int healPoints)
        {
            if (OnHeal != null) OnHeal(healPoints);
        }
        private void NotifyNewWeapon(WeaponType weaponType)
        {
            if (OnNewWeapon != null) OnNewWeapon(weaponType);
        }
        private void NotifyInvincible()
        {
            if (OnInvincible != null) OnInvincible();
        }
    }
}