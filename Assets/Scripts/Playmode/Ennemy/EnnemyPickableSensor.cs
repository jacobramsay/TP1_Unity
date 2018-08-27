using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Ennemy
{
    public delegate void EnemySensorEventHandler(EnnemyController ennemyController);

    public class EnnemyPickableSensor : MonoBehaviour
    {

        public event EnemySensorEventHandler OnEnemySensed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Enemy)) NotifyEnemySensed(other.GetComponent<EnnemyController>());
        }

        private void NotifyEnemySensed(EnnemyController ennemyController)
        {
            if (OnEnemySensed != null) OnEnemySensed(ennemyController);
        }
    }
}

