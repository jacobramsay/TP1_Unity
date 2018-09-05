using System;
using System.Collections;
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
        [Header("Options specifications")] [SerializeField] private float invicibilityCountdown;
    
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
        private Color normalColor;
        private Color collideColor;
        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
            CreateStartingWeapon();
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
            if (invicibilityCountdown == 0.0f)
                throw new ArgumentException("Invicibility countdown must be provided.");           
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

            ennemyDiedEventChannel = GameObject.FindWithTag(Tags.GameController).GetComponent<EnnemyDiedEventChannel>();
            gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();
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
            AttachEvents();
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
            DetachEvents();
        }

        public void Configure(EnnemyStrategy strategy, Color color)
        {
            body.GetComponent<SpriteRenderer>().color = color;
            sight.GetComponent<SpriteRenderer>().color = color;
            normalColor = color;
            collideColor = Color.red;
            strategyType = strategy;
            ConfigureStrategy();                 
        }
        private void OnHeal(int healPoints)
        {
            health.Heal(healPoints);
        }

        private void OnHit(int hitPoints)
        {
            health.Hit(hitPoints);
            StartCoroutine(FlashHit());
        }

        private void OnNewWeapon(WeaponType weaponType)
        {           
            handController.TakeWeapon(weaponType);
        }
        private void OnInvincible()
        {
            ActivateInvicibiliyCountdown();           
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
      
        private void OnPickableSeen(PickableController pickable)
        {
            strategy.PickableDetected(pickable);
        }       
        private IEnumerator FlashHit()
        {
            body.GetComponent<SpriteRenderer>().color = collideColor;
            yield return new WaitForSeconds(.1f);
            body.GetComponent<SpriteRenderer>().color = normalColor;
        }
        private IEnumerator InvicibilityRoutine(float countdownValue)
        {
            hitSensor.OnHit -= OnHit;
            yield return new WaitForSeconds(countdownValue);
            hitSensor.OnHit += OnHit;
        }
        private void AttachEvents()
        {
            ennemySensor.OnEnnemySeen += OnEnnemySeen;            
            pickableSensor.OnPickableSeen += OnPickableSeen;           
            hitSensor.OnHit += OnHit;
            bonusSensor.OnHeal += OnHeal;
            bonusSensor.OnNewWeapon += OnNewWeapon;
            bonusSensor.OnInvincible += OnInvincible;
            health.OnDeath += OnDeath;
        }
        private void DetachEvents()
        {
            ennemySensor.OnEnnemySeen -= OnEnnemySeen;           
            pickableSensor.OnPickableSeen -= OnPickableSeen;          
            hitSensor.OnHit -= OnHit;
            bonusSensor.OnHeal -= OnHeal;
            bonusSensor.OnNewWeapon -= OnNewWeapon;
            bonusSensor.OnInvincible -= OnInvincible;
            health.OnDeath -= OnDeath;
        }

        private void ActivateInvicibiliyCountdown()
        {
            StartCoroutine(InvicibilityRoutine(invicibilityCountdown));
        }
        private void ConfigureStrategy()
        {
            switch (strategyType)
            {
                case EnnemyStrategy.Careful:
                    this.strategy = new CarefulStrategy(mover, handController);
                    typeSign.GetComponent<SpriteRenderer>().sprite = carefulSprite;
                    break;
                case EnnemyStrategy.Cowboy:
                    this.strategy = new CowboyStrategy(mover, handController, false);
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
    }
}