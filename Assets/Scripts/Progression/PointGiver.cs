using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Progression
{
    public class PointGiver : Action
    {
        [Tooltip("How many points are awarded when this action is triggered.")]
        [SerializeField] private int defaultPoints = 100;

        private PointsCounter pointsCounter;

        private void Start() { pointsCounter = FindObjectOfType<PointsCounter>(); }

        override public void DoAction() { pointsCounter.AddPoints(defaultPoints); }

        override public void DoAction(int value) { pointsCounter.AddPoints(value); }
    }
}