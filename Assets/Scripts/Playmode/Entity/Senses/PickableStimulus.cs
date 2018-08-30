﻿
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class PickableStimulus : MonoBehaviour
    {
        private PickableController pickable;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pickable = GetComponent<PickableController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<PickableSensor>()?.See(pickable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<PickableSensor>()?.LooseSightOf(pickable);
        }
    }
}