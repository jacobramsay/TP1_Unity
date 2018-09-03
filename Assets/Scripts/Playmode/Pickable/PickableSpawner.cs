
using Playmode.Util.Collections;
using Playmode.Util.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Playmode.Pickable
{
    public class PickableSpawner : MonoBehaviour
    {
        private GameController gameController;

        private static readonly PickableType[] PickableTypes =
            {
            PickableType.MedicalKit,
            PickableType.Shotgun,
            PickableType.Uzi,
            PickableType.Invincible
            
        };

        [SerializeField] private GameObject pickablePrefab;
        [SerializeField] private float minSpawnDelayInSeconds = 3f;
        [SerializeField] private float maxSpawnDelayInSeconds = 10f;
  

        public void ActivateSpawnPoint()
        {
            StartCoroutine(SpawnPrefabsRoutine());
        }
        private void OnEnable()
        {
            ActivateSpawnPoint();
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
        private void Awake()
        {
            ValidateSerialisedFields();
            gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();
        }
        private void ValidateSerialisedFields()
        {
            if (pickablePrefab == null)
                throw new ArgumentException("Can't spawn null pickable prefab.");                       
        }
        private IEnumerator SpawnPrefabsRoutine()
        {                                  
           var rdmSpawnDelayInSeconds = UnityEngine.Random.Range(minSpawnDelayInSeconds, maxSpawnDelayInSeconds);
           yield return new WaitForSeconds(rdmSpawnDelayInSeconds);
            if (gameController.IsGameStarted && !gameController.IsGameOver)
            {
                SpawnRandomPickable();
            }                                              
        }
        private void SpawnRandomPickable()
        {
            var rdmPickableTypeIndex = UnityEngine.Random.Range(0, PickableTypes.Length);
            var pickableTypeProvider = PickableTypes[rdmPickableTypeIndex];           
            SpawnPickable(transform.position,
                    pickableTypeProvider);            
        }
        
        private void SpawnPickable(Vector3 position, PickableType type)
        {        
            Instantiate(pickablePrefab, position, Quaternion.identity).GetComponent<PickableController>().Configure(type, this);                           
        }

    }

}
