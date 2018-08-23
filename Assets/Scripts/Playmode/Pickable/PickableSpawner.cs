
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

        private void OnEnable()
        {
            StartCoroutine(SpawnPrefabsRoutine());
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
        private void Awake()
        {
            ValidateSerialisedFields();
        }
        private void ValidateSerialisedFields()
        {
            if (pickablePrefab == null)
                throw new ArgumentException("Can't spawn null pickable prefab.");                       
        }
        private IEnumerator SpawnPrefabsRoutine()
        {
            while (true)
            {
                var rdmSpawnDelayInSeconds = UnityEngine.Random.Range(minSpawnDelayInSeconds, maxSpawnDelayInSeconds);
                yield return new WaitForSeconds(rdmSpawnDelayInSeconds);
                SpawnRandomPickable();
            }
        }
        private void SpawnRandomPickable()
        {
            var rdmPickableTypeIndex = UnityEngine.Random.Range(0, PickableTypes.Length - 1);
            var pickableTypeProvider = PickableTypes[rdmPickableTypeIndex];           
            SpawnPickable(transform.position,
                    pickableTypeProvider);
        }
        
        private void SpawnPickable(Vector3 position, PickableType type)
        {
            Instantiate(pickablePrefab, position, Quaternion.identity);
        }
    }

}
