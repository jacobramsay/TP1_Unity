using System;
using Assets.Scripts.Playmode.Weapon;
using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Destruction;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using Playmode.Pickable;
using Playmode.Util.Values;
using Playmode.Weapon;
using UnityEngine;

namespace Playmode.Ennemy
{
    public class EnnemyController : MonoBehaviour
    {
        [Header("Body Parts")] [SerializeField] private GameObject body;
        [SerializeField] private GameObject hand;
        [SerializeField] private GameObject sight;
        [SerializeField] private GameObject typeSign;
        [Header("Type Images")] [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite carefulSprite;
        [SerializeField] private Sprite cowboySprite;
        [SerializeField] private Sprite camperSprite;
        [Header("Behaviour")] [SerializeField] private GameObject startingWeaponPrefab;

        private GameController gameController;
        private Health health;
        private Mover mover;
        private Destroyer destroyer;
        private PickableSensor pickableSensor;
        private EnnemySensor ennemySensor;
        private HitSensor hitSensor;
        private BonusSensor bonusSensor;
        private HandController handController;
        private EnnemyDiedEventChannel ennemyDiedEventChannel;

        private EnnemyStrategy strategyType;
        private IEnnemyStrategy strategy;


        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
            CreateStartingWeapon();

            ennemyDiedEventChannel = GameObject.FindWithTag(Tags.GameController).GetComponent<EnnemyDiedEventChannel>();
            gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();


        }

        private void ValidateSerialisedFields()
        {
            if (body == null)
                throw new ArgumentException("Body parts must be provided. Body is missing.");
            if (hand == null)
                throw new ArgumentException("Body parts must be provided. Hand is missing.");
            if (sight == null)
                throw new ArgumentException("Body parts must be provided. Sight is missing.");
            if (typeSign == null)
                throw new ArgumentException("Body parts must be provided. TypeSign is missing.");
            if (normalSprite == null)
                throw new ArgumentException("Type sprites must be provided. Normal is missing.");
            if (carefulSprite == null)
                throw new ArgumentException("Type sprites must be provided. Careful is missing.");
            if (cowboySprite == null)
                throw new ArgumentException("Type sprites must be provided. Cowboy is missing.");
            if (camperSprite == null)
                throw new ArgumentException("Type sprites must be provided. Camper is missing.");
            if (startingWeaponPrefab == null)
                throw new ArgumentException("StartingWeapon prefab must be provided.");
        }

        private void InitializeComponent()
        {
            health = GetComponent<Health>();
            mover = GetComponent<RootMover>();
            destroyer = GetComponent<RootDestroyer>();

            var rootTransform = transform.root;
            ennemySensor = rootTransform.GetComponentInChildren<EnnemySensor>();
            pickableSensor = rootTransform.GetComponentInChildren<PickableSensor>();
            hitSensor = rootTransform.GetComponentInChildren<HitSensor>();
            bonusSensor = rootTransform.GetComponentInChildren<BonusSensor>();
            handController = hand.GetComponent<HandController>();                       
        }

        private void CreateStartingWeapon()
        {
            handController.Hold(Instantiate(
                startingWeaponPrefab,
                Vector3.zero,
                Quaternion.identity
            ));
        }

        private void OnEnable()
        {
            
            ennemySensor.OnEnnemySeen += OnEnnemySeen;
            ennemySensor.OnEnnemySightLost += OnEnnemySightLost;
            pickableSensor.OnPickableSeen += OnPickableSeen;
            pickableSensor.OnPickableSightLost += OnPickableSightLost;
            hitSensor.OnHit += OnHit;
            bonusSensor.OnHeal += OnHeal;
            bonusSensor.OnNewWeapon += OnNewWeapon;
            health.OnDeath += OnDeath;
        }

        private void Update()
        {
            if (gameController.IsGameStarted && !gameController.IsGameOver)
            {
                strategy.Act();
            }
        }

        private void OnDisable()
        {
            ennemySensor.OnEnnemySeen -= OnEnnemySeen;
            ennemySensor.OnEnnemySightLost -= OnEnnemySightLost;
            pickableSensor.OnPickableSeen -= OnPickableSeen;
            pickableSensor.OnPickableSightLost -= OnPickableSightLost;
            hitSensor.OnHit -= OnHit;
            bonusSensor.OnHeal -= OnHeal;
            bonusSensor.OnNewWeapon -= OnNewWeapon;
            health.OnDeath -= OnDeath;
        }

        public void Configure(EnnemyStrategy strategy, Color color)
        {
            body.GetComponent<SpriteRenderer>().color = color;
            sight.GetComponent<SpriteRenderer>().color = color;
            strategyType = strategy;
            switch (strategyType)
            {
                case EnnemyStrategy.Careful:
                    this.strategy = new CarefulStrategy(mover, handController);
                    typeSign.GetComponent<SpriteRenderer>().sprite = carefulSprite;
                    break;
                case EnnemyStrategy.Cowboy:
                    this.strategy = new CowboyStrategy(mover, handController,false);
                    typeSign.GetComponent<SpriteRenderer>().sprite = cowboySprite;
                    break;
                case EnnemyStrategy.Camper:
                    this.strategy = new CamperStrategy(mover, handController);
                    typeSign.GetComponent<SpriteRenderer>().sprite = camperSprite;
                    break;
                default:
                    this.strategy = new NormalStrategy(mover, handController);
                    typeSign.GetComponent<SpriteRenderer>().sprite = normalSprite;
                    break;
            }
        }
        private void OnHeal(int healPoints)
        {
            health.Heal(healPoints);
        }

        private void OnHit(int hitPoints)
        {
            health.Hit(hitPoints);
        }

        private void OnNewWeapon(WeaponType weaponType)
        {           
            handController.TakeWeapon(weaponType);
        }

        private void OnDeath()
        {
            ennemyDiedEventChannel.Publish();
            destroyer.Destroy();
        }

        private void OnEnnemySeen(EnnemyController ennemy)
        {
            strategy.UpdateTarget(ennemy.body);
        }

        private void OnEnnemySightLost(EnnemyController ennemy)
        {    
            
        }

        private void OnPickableSeen(PickableController pickable)
        {
            strategy.PickableDetected(pickable);
        }

        private void OnPickableSightLost(PickableController pickable)
        {

        }      
    }
}