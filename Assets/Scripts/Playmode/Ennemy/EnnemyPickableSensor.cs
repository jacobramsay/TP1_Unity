using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EnemySensorEventHandler();

public class EnnemyPickableSensor : MonoBehaviour {

    public event EnemySensorEventHandler OnEnemySensed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Enemy)) NotifyEnemySensed();
    }

    private void NotifyEnemySensed()
    {
        if (OnEnemySensed != null) OnEnemySensed();
    }
}
