using UnityEngine;
using UnityEditor;
using Playmode.Pickable;
using System.Collections.Generic;

namespace Playmode.Entity.Senses
{
    public delegate void PickableSensorEventHandler(PickableController pickable);

    public class PickableSensor : MonoBehaviour
    {
        private ICollection<PickableController> pickablesInSight;

        public event PickableSensorEventHandler OnPickableSeen;
        public event PickableSensorEventHandler OnPickableSightLost;

        public IEnumerable<PickableController> PickablesInSight => pickablesInSight;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pickablesInSight = new HashSet<PickableController>();
        }

        public void See(PickableController pickable)
        {
            pickablesInSight.Add(pickable);

            NotifyPickableSeen(pickable);
        }

        public void LooseSightOf(PickableController pickable)
        {
            pickablesInSight.Remove(pickable);

            NotifyPickableSightLost(pickable);
        }

        private void NotifyPickableSeen(PickableController pickable)
        {
            if (OnPickableSeen != null) OnPickableSeen(pickable);
        }

        private void NotifyPickableSightLost(PickableController pickable)
        {
            if (OnPickableSightLost != null) OnPickableSightLost(pickable);
        }
    }
}