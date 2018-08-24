
using Playmode.Util.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Playmode.Pickable
{
    public class PickableSpawner : MonoBehaviour
    {

        private static readonly PickableType[] PickableTypes =
            {
            PickableType.MedicalKit,
            PickableType.Shotgun,
            PickableType.Uzi
            
        };

        [SerializeField] private GameObject pickablePrefab;
        [SerializeField] private float minSpawnDelayInSeconds = 3f;
        [SerializeField] private float maxSpawnDelayInSeconds = 10f;

        private bool available;
        private EnnemyPickableSensor ennemyPickableSensor;

        private void OnEnable()
        {
            available = true;
            StartCoroutine(SpawnPrefabsRoutine());
            ennemyPickableSensor.OnEnemySensed += NotifyEnnemyPickup;
        }
        private void OnDisable()
        {
            StopAllCoroutines();
            ennemyPickableSensor.OnEnemySensed -= NotifyEnnemyPickup;
        }
        private void Awake()
        {
            ValidateSerialisedFields();
            ennemyPickableSensor = GetComponent<EnnemyPickableSensor>();
        }
        private void ValidateSerialisedFields()
        {
            if (pickablePrefab == null)
                throw new ArgumentException("Can't spawn null pickable prefab.");                       
        }
        private IEnumerator SpawnPrefabsRoutine()
        {
            if(available)
            {
                while (true)
                {
                    var rdmSpawnDelayInSeconds = UnityEngine.Random.Range(minSpawnDelayInSeconds, maxSpawnDelayInSeconds);
                    yield return new WaitForSeconds(rdmSpawnDelayInSeconds);
                    SpawnRandomPickable();
                }
            }       
        }
        private void SpawnRandomPickable()
        {
            var rdmPickableTypeIndex = UnityEngine.Random.Range(0, PickableTypes.Length);
            var pickableTypeProvider = PickableTypes[rdmPickableTypeIndex];           
            SpawnPickable(transform.position,
                    pickableTypeProvider);

            Debug.Log(rdmPickableTypeIndex);
            Debug.Log(pickableTypeProvider);
        }
        
        private void SpawnPickable(Vector3 position, PickableType type)
        {
            Instantiate(pickablePrefab, position, Quaternion.identity).GetComponent<PickableController>().Configure(type);
            available = false;
        }

        private void NotifyEnnemyPickup()
        {
           available = true;
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
    }

}
