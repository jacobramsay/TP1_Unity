
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

        public bool available { get; set; }

        private void OnEnable()
        {
            available = true;
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
                    if(available)
                    {
                        var rdmSpawnDelayInSeconds = UnityEngine.Random.Range(minSpawnDelayInSeconds, maxSpawnDelayInSeconds);
                        yield return new WaitForSeconds(rdmSpawnDelayInSeconds);
                        SpawnRandomPickable();
                }
                    else
                    {
                        yield return new WaitForSeconds(0.1f);
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
            if (available == true)
            {
                Instantiate(pickablePrefab, position, Quaternion.identity).GetComponent<PickableController>().Configure(type, this);
                available = false;
            }
        }

    }

}
